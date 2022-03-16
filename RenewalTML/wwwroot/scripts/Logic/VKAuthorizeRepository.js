import { createObjectError } from '../System/ErrorCatchingUtils.js';

export function AuthorizeVk(instance) {
    try {
        VK.Auth.getLoginStatus(function (response) {
            if (response.session) {
                // Если пользователь уже был авторизован когда-то, не запускаем popup окно, а сразу авторизируем пользователя
                // Предположительно пользователь всегда должен быть зарегистрирован уже, но на всякий лучше сделать проверку
                instance.invokeMethodAsync("VKAuthorizeComplete", JSON.stringify(response));
            } else {
                VK.Auth.login(function (response) {
                    if (response.session) {
                        // В целом тоже самое, что и просто вывод, но с открытием popup окна.
                        instance.invokeMethodAsync("VKAuthorizeComplete", JSON.stringify(response));

                    } else {

                        instance.invokeMethodAsync("VKAuthorizeComplete", createObjectError('-user-vkAuth-declaim', 'info'));
                        //return JSON.stringify('-declaim-authorize');
                        // Пользователь нажал кнопку Отмена в окне авторизации 
                    }
                });
            }
        });
    }
    catch (e) {
        instance.invokeMethodAsync("VKAuthorizeComplete", createObjectError('-user-vkAuth-error', 'error'));
    }
}

/* OTHER FUNCTIONS */

$(function () {
    /* INIT APPLICATION */
    VK.init({
        apiId: 7912543
    });
});
