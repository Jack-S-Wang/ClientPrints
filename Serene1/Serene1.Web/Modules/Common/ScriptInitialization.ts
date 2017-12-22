/// <reference path="../Common/Helpers/LanguageList.ts" />

namespace Serene1.ScriptInitialization {
    Q.Config.responsiveDialogs = true;
    Q.Config.rootNamespaces.push('Serene1');
    Serenity.EntityDialog.defaultLanguageList = LanguageList.getValue;
}