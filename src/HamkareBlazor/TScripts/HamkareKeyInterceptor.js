// Copyright (c) HamkareBlazor 2021
// HamkareBlazor licenses this file to you under the MIT license.
// See the LICENSE file in the project root for more information.

/**
 * Factory that resolves elements and manages HamkareKeyInterceptor instances.
 * Exposes connect/update/disconnect entry points for .NET interop.
 */
class HamkareKeyInterceptorFactory {
    /**
     * Creates (or reuses) a key interceptor for an element and attaches handlers.
     */
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

    /**
     * Updates the key option for an existing interceptor registration.
     */
    updatekey(elementId, option) {
        const element = document.getElementById(elementId);
        if (!element?.hamkareKeyInterceptor)
            return;
        element.hamkareKeyInterceptor.updatekey(option);
    }

    /**
     * Detaches a key interceptor from an element.
     */
    disconnect(elementId) {
        const element = document.getElementById(elementId);
        if (!element?.hamkareKeyInterceptor)
            return;
        element.hamkareKeyInterceptor.disconnect();
    }
}
window.hamkareKeyInterceptor = new HamkareKeyInterceptorFactory();

/**
 * Applies key options and raises keyboard callbacks to .NET.
 * Handles preventDefault/stopPropagation in JS before component handlers run.
 */
class HamkareKeyInterceptor {
    constructor(dotNetRef, options) {
        this._dotNetRef = dotNetRef;
        this._options = options;
        this.logger = options.enableLogging ? console.log : () => { };
        this.logger('[HamkareBlazor | KeyInterceptor] Interceptor initialized', { options });
    }

    /**
     * Starts key interception on the target element (or matching child elements).
     */
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
        // transform key options into a key lookup
        this._keyOptions = {};
        this._regexOptions = [];
        for (const keyOption of this._options.keys) {
            if (!keyOption?.key) {
                this.logger('[HamkareBlazor | KeyInterceptor] got invalid key options: ', keyOption);
                continue;
            }
            this.setKeyOption(keyOption);
        }
        this.logger('[HamkareBlazor | KeyInterceptor] key options: ', this._keyOptions);
        if (this._regexOptions.length > 0)
            this.logger('[HamkareBlazor | KeyInterceptor] regex options: ', this._regexOptions);
        // register delegated handlers once on the root element
        this.attachHandlers(this._element);
    }

    /**
     * Normalizes and stores one key option definition.
     */
    setKeyOption(keyOption) {
        if (keyOption.key.length > 2 && keyOption.key.startsWith('/') && keyOption.key.endsWith('/')) {
            // JS regex key options such as "/[a-z]/" or "/a|b/" but NOT "/[a-z]/g" or "/[a-z]/i"
            keyOption.regex = new RegExp(keyOption.key.substring(1, keyOption.key.length - 1)); // strip the / from start and end
            this._regexOptions.push(keyOption);
        }
        else
            // Normalize direct lookups to lowercase once so event handlers can stay allocation-light.
            this._keyOptions[keyOption.key.toLowerCase()] = keyOption;
        // remove whitespace and enforce lowercase
        const whitespace = new RegExp("\\s", "g");
        keyOption.preventDown = (keyOption.preventDown || "none").replace(whitespace, "").toLowerCase();
        keyOption.preventUp = (keyOption.preventUp || "none").replace(whitespace, "").toLowerCase();
        keyOption.stopDown = (keyOption.stopDown || "none").replace(whitespace, "").toLowerCase();
        keyOption.stopUp = (keyOption.stopUp || "none").replace(whitespace, "").toLowerCase();
    }

    /**
     * Updates an existing key option definition.
     */
    updatekey(updatedOption) {
        const option = this._keyOptions[updatedOption.key.toLowerCase()];
        option || this.logger('[HamkareBlazor | KeyInterceptor] updating option failed: key not registered');
        this.setKeyOption(updatedOption);
        this.logger('[HamkareBlazor | KeyInterceptor] updated option ', { option, updatedOption });
    }

    /**
     * Stops interception and detaches all listeners.
     */
    disconnect() {
        if (!this._isConnected)
            return;
        this.logger('[HamkareBlazor | KeyInterceptor] disconnect delegated event handlers');
        this.detachHandlers(this._element);
        this._isConnected = false;
        this._element = null;
    }

    /**
     * Attaches keydown/keyup handlers to a target element.
     */
    attachHandlers(element) {
        this.logger('[HamkareBlazor | KeyInterceptor] attaching delegated handlers ', { element });
        if (this._delegatedHandlersAttached)
            return;
        element.hamkareKeyInterceptor = this;
        element.addEventListener('keydown', this.onKeyDown);
        element.addEventListener('keyup', this.onKeyUp);
        this._delegatedHandlersAttached = true;
    }

    /**
     * Detaches keydown/keyup handlers from a target element.
     */
    detachHandlers(element) {
        this.logger('[HamkareBlazor | KeyInterceptor] detaching delegated handlers ', { element });
        if (!this._delegatedHandlersAttached)
            return;
        element.removeEventListener('keydown', this.onKeyDown);
        element.removeEventListener('keyup', this.onKeyUp);
        if (element.hamkareKeyInterceptor === this)
            delete element.hamkareKeyInterceptor;
        this._delegatedHandlersAttached = false;
    }

    /**
     * Determines whether a delegated event should be handled by this interceptor.
     */
    shouldHandleEvent(args) {
        const self = this.hamkareKeyInterceptor; // func is invoked with this == element that owns delegated handlers
        if (!self?._isConnected || !self._element)
            return false;
        const targetClass = self._options.targetClass;
        if (!targetClass)
            return true;

        let current = args.target;
        if (current?.nodeType !== Node.ELEMENT_NODE)
            current = current?.parentElement;
        while (current && current !== self._element) {
            if (current.classList?.contains(targetClass))
                return true;
            current = current.parentElement;
        }
        return false;
    }

    /**
     * Checks whether current modifier state matches an option expression.
     */
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

    /**
     * Processes keydown behavior and invokes .NET when configured.
     */
    onKeyDown(args) {
        const self = this.hamkareKeyInterceptor; // func is invoked with this == child
        if (!self.shouldHandleEvent.call(this, args))
            return;
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
            // Regex options allow wildcard key rules without precomputing every key in JS.
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

    /**
     * Applies preventDefault/stopPropagation rules for keydown.
     */
    processKeyDown(args, keyOptions) {
        if (this.matchesKeyCombination(keyOptions.preventDown, args))
            args.preventDefault();
        if (this.matchesKeyCombination(keyOptions.stopDown, args))
            args.stopPropagation();
    }

    /**
     * Returns whether keydown should be forwarded to .NET.
     */
    shouldInvokeKeyDown(args, keyOptions) {
        return keyOptions.subscribeDown && (!keyOptions.ignoreDownRepeats || !args.repeat);
    }

    /**
     * Processes keyup behavior and invokes .NET when configured.
     */
    onKeyUp(args) {
        const self = this.hamkareKeyInterceptor; // func is invoked with this == child
        if (!self.shouldHandleEvent.call(this, args))
            return;
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

    /**
     * Applies preventDefault/stopPropagation rules for keyup.
     */
    processKeyUp(args, keyOptions) {
        if (this.matchesKeyCombination(keyOptions.preventUp, args))
            args.preventDefault();
        if (this.matchesKeyCombination(keyOptions.stopUp, args))
            args.stopPropagation();
    }

    /**
     * Converts a DOM keyboard event to the .NET keyboard event payload shape.
     */
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
