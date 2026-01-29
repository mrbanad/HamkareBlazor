// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

class HamkareJsEventFactory {
    connect(dotNetRef, elementId, options) {
        //console.log('[HamkareBlazor | HamkareJsEventFactory] connect ', { dotNetRef, elementId, options });
        if (!elementId)
            throw "[HamkareBlazor | JsEvent] elementId: expected element id!";
        const element = document.getElementById(elementId);
        if (!element)
            throw "[HamkareBlazor | JsEvent] no element found for id: " + elementId;
        if (!element.hamkareJsEvent)
            element.hamkareJsEvent = new HamkareJsEvent(dotNetRef, options);
        element.hamkareJsEvent.connect(element);
    }

    disconnect(elementId) {
        const element = document.getElementById(elementId);
        if (!element || !element.hamkareJsEvent)
            return;
        element.hamkareJsEvent.disconnect();
    }

    subscribe(elementId, eventName) {
        //console.log('[HamkareBlazor | HamkareJsEventFactory] subscribe ', { elementId, eventName});
        if (!elementId)
            throw "[HamkareBlazor | JsEvent] elementId: expected element id!";
        const element = document.getElementById(elementId);
        if (!element)
            throw "[HamkareBlazor | JsEvent] no element found for id: " +elementId;
        if (!element.hamkareJsEvent)
            throw "[HamkareBlazor | JsEvent] please connect before subscribing";
        element.hamkareJsEvent.subscribe(eventName);
    }

    unsubscribe(elementId, eventName) {
        const element = document.getElementById(elementId);
        if (!element || !element.hamkareJsEvent)
            return;
        element.hamkareJsEvent.unsubscribe(element, eventName);
    }
}
window.hamkareJsEvent = new HamkareJsEventFactory();


class HamkareJsEvent {

    constructor(dotNetRef, options) {
        this._dotNetRef = dotNetRef;
        this._options = options || {};
        this.logger = options.enableLogging ? console.log : () => { };
        this.logger('[HamkareBlazor | JsEvent] Initialized', { options });
        this._subscribedEvents = {};
    }

    connect(element) {
        if (!this._options)
            return;
        if (!this._options.targetClass)
            throw "_options.targetClass: css class name expected";
        if (this._observer) {
            // don't do double registration
            return;
        }
        const targetClass = this._options.targetClass;
        this.logger('[HamkareBlazor | JsEvent] Start observing DOM of element for changes to child with class ', { element, targetClass });
        this._element = element;
        this._observer = new MutationObserver(this.onDomChanged);
        this._observer.hamkareJsEvent = this;
        this._observer.observe(this._element, { attributes: false, childList: true, subtree: true });
        this._observedChildren = [];
    }

    disconnect() {
        if (!this._observer)
            return;
        this.logger('[HamkareBlazor | JsEvent] disconnect mutation observer and event handler ');
        this._observer.disconnect();
        this._observer = null;
        for (const child of this._observedChildren)
            this.detachHandlers(child);
    }

    subscribe(eventName) {
        // register handlers
        if (this._subscribedEvents[eventName]) {
            //console.log("... already attached");
            return;
        }
        const element = this._element;
        const targetClass = this._options.targetClass;
        //this.logger('[HamkareBlazor | JsEvent] Subscribe event ' + eventName, { element, targetClass });
        this._subscribedEvents[eventName]=true;
        for (const child of element.getElementsByClassName(targetClass)) {
            this.attachHandlers(child);
        }
    }

    unsubscribe(eventName) {
        if (!this._observer)
            return;
        this.logger('[HamkareBlazor | JsEvent] unsubscribe event handler ' + eventName );
        this._observer.disconnect();
        this._observer = null;
        this._subscribedEvents[eventName] = false;
        for (const child of this._observedChildren) {
            this.detachHandler(child, eventName);
        }
    }

    attachHandlers(child) {
        child.hamkareJsEvent = this;
        //this.logger('[HamkareBlazor | JsEvent] attachHandlers ', this._subscribedEvents, child);
        for (const eventName of Object.getOwnPropertyNames(this._subscribedEvents)) {
            if (!this._subscribedEvents[eventName])
                continue;
            // note: multiple registration of the same event not possible due to the use of the same handler func
            this.logger('[HamkareBlazor | JsEvent] attaching event ' + eventName, child);
            child.addEventListener(eventName, this.eventHandler);
        }
        if(this._observedChildren.indexOf(child) < 0)
            this._observedChildren.push(child);
    }

    detachHandler(child, eventName) {
        this.logger('[HamkareBlazor | JsEvent] detaching handler ' + eventName, child);
        child.removeEventListener(eventName, this.eventHandler);
    }

    detachHandlers(child) {
        this.logger('[HamkareBlazor | JsEvent] detaching handlers ', child);
        for (const eventName of Object.getOwnPropertyNames(this._subscribedEvents)) {
            if (!this._subscribedEvents[eventName])
                continue;
            child.removeEventListener(eventName, this.eventHandler);
        }
        this._observedChildren = this._observedChildren.filter(x=>x!==child);
    }

    onDomChanged(mutationsList, _) {
        const self = this.hamkareJsEvent; // func is invoked with this == _observer
        //self.logger('[HamkareBlazor | JsEvent] onDomChanged: ', { self });
        const targetClass = self._options.targetClass;
        for (const mutation of mutationsList) {
            //self.logger('[HamkareBlazor | JsEvent] Subtree mutation: ', { mutation });
            for (const element of mutation.addedNodes) {
                if (element.classList && element.classList.contains(targetClass)) {
                    if (!self._options.TagName || element.tagName == self._options.TagName)
                        self.attachHandlers(element);
                }
            }
            for (const element of mutation.removedNodes) {
                if (element.classList && element.classList.contains(targetClass)) {
                    if (!self._options.tagName || element.tagName == self._options.tagName)
                         self.detachHandlers(element);
                }
            }
        }
    }

    eventHandler(e) {
        const self = this.hamkareJsEvent; // func is invoked with this == child
        const eventName = e.type;
        self.logger('[HamkareBlazor | JsEvent] "' + eventName + '"', e);
        // call specific handler
        self["on" + eventName](self, e);
    }

    onkeyup(self, e) {
        const caretPosition = e.target.selectionStart;
        const invoke = self._subscribedEvents["keyup"];
        if (invoke) {
            //self.logger('[HamkareBlazor | JsEvent] caret pos: ' + caretPosition);
            self._dotNetRef.invokeMethodAsync('OnCaretPositionChanged', caretPosition);
        }
    }

    onclick(self, e) {
        const caretPosition = e.target.selectionStart;
        const invoke = self._subscribedEvents["click"];
        if (invoke) {
            //self.logger('[HamkareBlazor | JsEvent] caret pos: ' + caretPosition);
            self._dotNetRef.invokeMethodAsync('OnCaretPositionChanged', caretPosition);
        }
    }

    //oncopy(self, e) {
    //    const invoke = self._subscribedEvents["copy"];
    //    if (invoke) {
    //        //self.logger('[HamkareBlazor | JsEvent] copy (preventing default and stopping propagation)');
    //        e.preventDefault();
    //        e.stopPropagation();
    //        self._dotNetRef.invokeMethodAsync('OnCopy');
    //    }
    //}

    onpaste(self, e) {
        const invoke = self._subscribedEvents["paste"];
        if (invoke) {
            //self.logger('[HamkareBlazor | JsEvent] paste (preventing default and stopping propagation)');
            e.preventDefault();
            e.stopPropagation();
            const clipboardData = ((e.originalEvent || e).clipboardData || window.clipboardData);
            if (!clipboardData) {
                self.logger('[HamkareBlazor | JsEvent] clipboardData is null', e);
                return;
            }
            const text = clipboardData.getData('text/plain');
            self._dotNetRef.invokeMethodAsync('OnPaste', text);
        }
    }

    onselect(self, e) {
        const invoke = self._subscribedEvents["select"];
        if (invoke) {
            const start = e.target.selectionStart;
            const end = e.target.selectionEnd;
            if (start === end)
                return; // <-- we have caret position changed for that.
            //self.logger('[HamkareBlazor | JsEvent] select ' + start + "-" + end);
            self._dotNetRef.invokeMethodAsync('OnSelect', start, end);
        }
    }
}

