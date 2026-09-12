window.cookieHelper = {
    get: function (name) {
        const cookies = document.cookie.split(';');

        for (const cookie of cookies) {
            const [key, ...value] = cookie.trim().split('=');

            if (key === name) {
                return decodeURIComponent(value.join('='));
            }
        }

        return null;
    },

    set: function (name, value, days) {
        let cookie = `${name}=${encodeURIComponent(value)}; path=/`;

        if (days != null) {
            const expires = new Date();

            expires.setTime(
                expires.getTime() + days * 24 * 60 * 60 * 1000
            );

            cookie += `; expires=${expires.toUTCString()}`;
        }

        document.cookie = cookie;
    },

    delete: function (name) {
        document.cookie =
            `${name}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/`;
    }
};
