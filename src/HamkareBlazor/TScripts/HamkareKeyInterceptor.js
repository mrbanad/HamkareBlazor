// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

class HamkareKeyInterceptorFactory {

    connect(dotNetRef, elementId, options) {
        //console.log('[HamkareBlazor | HamkareKeyInterceptorFactory] connect ', { dotNetRef, element, options });
        if (!elementId)
            throw "elementId: expected element id!";
        const element = document.getElementById(elementId);
        if (!element)
            throw "no element found for id: " + elementId;
        if (!element.hamkareKeyInterceptor)
            element.hamkareKeyInterceptor = new HamkareKeyInterceptor(dotNetRef, options);
        element.hamkareKeyInterceptor.connect(element);
    }

    updatekey(elementId, option) {
        const element = document.getElementById(elementId);
        if (!element || !element.hamkareKeyInterceptor)
            return;
        element.hamkareKeyInterceptor.updatekey(option);
    }

    disconnect(elementId) {
        const element = document.getElementById(elementId);
        if (!element || !element.hamkareKeyInterceptor)
            return;
        element.hamkareKeyInterceptor.disconnect();
    }
}
window.hamkareKeyInterceptor = new HamkareKeyInterceptorFactory();

class HamkareKeyInterceptor {

    constructor(dotNetRef, options) {
        this._dotNetRef = dotNetRef;
        this._options = options;
        this.logger = options.enableLogging ? console.log : () => { };
        this.logger('[HamkareBlazor | KeyInterceptor] Interceptor initialized', { options });
    }

    connect(element) {
        if (!this._options)
            return;
        if (!this._options.keys)
            throw "_options.keys: array of KeyOptions expected";
        if (this._isConnected) {
            // don't do double registration
            return;
        }
        this._isConnected = true;
        this._element = element;
        const targetClass = this._options.targetClass;
        // changes to the DOM subtree only require observation when targeting child elements for target class
        if (targetClass) {
            this.logger('[HamkareBlazor | KeyInterceptor] Start observing DOM of element for changes to child with class ', { element, targetClass });
            this._observer = new MutationObserver(this.onDomChanged);
            this._observer.hamkareKeyInterceptor = this;
            this._observer.observe(this._element, { attributes: false, childList: true, subtree: true });
        }
        this._observedChildren = [];
        // transform key options into a key lookup
        this._keyOptions = {};
        this._regexOptions = [];
        for (const keyOption of this._options.keys) {
            if (!keyOption || !keyOption.key) {
                this.logger('[HamkareBlazor | KeyInterceptor] got invalid key options: ', keyOption);
                continue;
            }
            this.setKeyOption(keyOption);
        }
        this.logger('[HamkareBlazor | KeyInterceptor] key options: ', this._keyOptions);
        if (this._regexOptions.size > 0)
            this.logger('[HamkareBlazor | KeyInterceptor] regex options: ', this._regexOptions);
        // register handlers
        if (targetClass) {
            for (const child of this._element.getElementsByClassName(targetClass)) {
                this.attachHandlers(child);
            }
        } else {
            this.attachHandlers(this._element);
        }
    }

    setKeyOption(keyOption) {
        if (keyOption.key.length > 2 && keyOption.key.startsWith('/') && keyOption.key.endsWith('/')) {
            // JS regex key options such as "/[a-z]/" or "/a|b/" but NOT "/[a-z]/g" or "/[a-z]/i"
            keyOption.regex = new RegExp(keyOption.key.substring(1, keyOption.key.length - 1)); // strip the / from start and end
            this._regexOptions.push(keyOption);
        }
        else
            this._keyOptions[keyOption.key.toLowerCase()] = keyOption;
        // remove whitespace and enforce lowercase
        const whitespace = new RegExp("\\s", "g");
        keyOption.preventDown = (keyOption.preventDown || "none").replace(whitespace, "").toLowerCase();
        keyOption.preventUp = (keyOption.preventUp || "none").replace(whitespace, "").toLowerCase();
        keyOption.stopDown = (keyOption.stopDown || "none").replace(whitespace, "").toLowerCase();
        keyOption.stopUp = (keyOption.stopUp || "none").replace(whitespace, "").toLowerCase();
    }

    updatekey(updatedOption) {
        const option = this._keyOptions[updatedOption.key.toLowerCase()];
        option || this.logger('[HamkareBlazor | KeyInterceptor] updating option failed: key not registered');
        this.setKeyOption(updatedOption);
        this.logger('[HamkareBlazor | KeyInterceptor] updated option ', { option, updatedOption });
    }

    disconnect() {
        if (!this._isConnected)
            return;
        if (this._observer) {
            this.logger('[HamkareBlazor | KeyInterceptor] disconnect mutation observer and event handlers');
            this._observer.disconnect();
            this._observer = null;
        }
        for (const child of this._observedChildren)
            this.detachHandlers(child);
        this._isConnected = false;
    }

    attachHandlers(child) {
        this.logger('[HamkareBlazor | KeyInterceptor] attaching handlers ', { child });
        if (this._observedChildren.indexOf(child) > -1) {
            //console.log("... already attached");
            return;
        }
        child.hamkareKeyInterceptor = this;
        child.addEventListener('keydown', this.onKeyDown);
        child.addEventListener('keyup', this.onKeyUp);
        this._observedChildren.push(child);
    }

    detachHandlers(child) {
        this.logger('[HamkareBlazor | KeyInterceptor] detaching handlers ', { child });
        child.removeEventListener('keydown', this.onKeyDown);
        child.removeEventListener('keyup', this.onKeyUp);
        this._observedChildren = this._observedChildren.filter(x=>x!==child);
    }

    onDomChanged(mutationsList, _) {
        const self = this.hamkareKeyInterceptor; // func is invoked with this == _observer
        //self.logger('[HamkareBlazor | KeyInterceptor] onDomChanged: ', { self });
        const targetClass = self._options.targetClass;
        for (const mutation of mutationsList) {
            //self.logger('[HamkareBlazor | KeyInterceptor] Subtree mutation: ', { mutation });
            for (const element of mutation.addedNodes) {
                if (element.classList && element.classList.contains(targetClass))
                    self.attachHandlers(element);
            }
            for (const element of mutation.removedNodes) {
                if (element.classList && element.classList.contains(targetClass))
                    self.detachHandlers(element);
            }
        }
    }

    matchesKeyCombination(option, args) {
        if (!option || option === "none")
            return false;
        if (option === "any")
            return true;
        const shift = args.shiftKey;
        const ctrl = args.ctrlKey;
        const alt = args.altKey;
        const meta = args.metaKey;
        const any = shift || ctrl || alt || meta;
        if (any && option === "key+any")
            return true;
        if (!any && option.includes("key+none"))
            return true;
        if (!any)
            return false;
        const combi = `key${shift ? "+shift" : ""}${ctrl ? "+ctrl" : ""}${alt ? "+alt" : ""}${meta ? "+meta" : ""}`;
        return option.includes(combi);
    }

    onKeyDown(args) {
        const self = this.hamkareKeyInterceptor; // func is invoked with this == child
        if (!args.key) {
            self.logger('[HamkareBlazor | KeyInterceptor] key is undefined', args);
            return;
        }

        const key = args.key.toLowerCase();
        self.logger('[HamkareBlazor | KeyInterceptor] down "' + key + '"', args);
        let invoke = false;
        if (self._keyOptions.hasOwnProperty(key)) {
            const keyOptions = self._keyOptions[key];
            self.logger('[HamkareBlazor | KeyInterceptor] options for "' + key + '"', keyOptions);
            self.processKeyDown(args, keyOptions);
            if (self.shouldInvokeKeyDown(args, keyOptions))
                invoke = true;
        }
        for (const keyOptions of self._regexOptions) {
            if (keyOptions.regex.test(key)) {
                self.logger('[HamkareBlazor | KeyInterceptor] regex options for "' + key + '"', keyOptions);
                self.processKeyDown(args, keyOptions);
                if (self.shouldInvokeKeyDown(args, keyOptions))
                    invoke = true;
            }
        }
        if (invoke) {
            const eventArgs = self.toKeyboardEventArgs(args);
            eventArgs.Type = "keydown";
            self._dotNetRef.invokeMethodAsync('OnKeyDown', self._element.id, eventArgs);
        }
    }

    processKeyDown(args, keyOptions) {
        if (this.matchesKeyCombination(keyOptions.preventDown, args))
            args.preventDefault();
        if (this.matchesKeyCombination(keyOptions.stopDown, args))
            args.stopPropagation();
    }

    shouldInvokeKeyDown(args, keyOptions) {
        return keyOptions.subscribeDown && (!keyOptions.ignoreDownRepeats || !args.repeat);
    }

    onKeyUp(args) {
        const self = this.hamkareKeyInterceptor; // func is invoked with this == child
        if (!args.key) {
            self.logger('[HamkareBlazor | KeyInterceptor] key is undefined', args);
            return;
        }

        const key = args.key.toLowerCase();
        self.logger('[HamkareBlazor | KeyInterceptor] up "' + key + '"', args);
        let invoke = false;
        if (self._keyOptions.hasOwnProperty(key)) {
            const keyOptions = self._keyOptions[key];
            self.processKeyUp(args, keyOptions);
            if (keyOptions.subscribeUp)
                invoke = true;
        }
        for (const keyOptions of self._regexOptions) {
            if (keyOptions.regex.test(key)) {
                self.processKeyUp(args, keyOptions);
                if (keyOptions.subscribeUp)
                    invoke = true;
            }
        }
        if (invoke) {
            const eventArgs = self.toKeyboardEventArgs(args);
            eventArgs.Type = "keyup";
            self._dotNetRef.invokeMethodAsync('OnKeyUp', self._element.id, eventArgs);
        }
    }

    processKeyUp(args, keyOptions) {
        if (this.matchesKeyCombination(keyOptions.preventUp, args))
            args.preventDefault();
        if (this.matchesKeyCombination(keyOptions.stopUp, args))
            args.stopPropagation();
    }

    toKeyboardEventArgs(args) {
        return {
            Key: args.key,
            Code: args.code,
            Location: args.location,
            Repeat: args.repeat,
            CtrlKey: args.ctrlKey,
            ShiftKey: args.shiftKey,
            AltKey: args.altKey,
            MetaKey: args.metaKey
        };
    }

}

