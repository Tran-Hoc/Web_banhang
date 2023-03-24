﻿
// Documentation for client options:
// https://github.com/Studio-42/elFinder/wiki/Client-configuration-options
$(document).ready(function () {
    var myCommands = elFinder.prototype._options.commands;

    // Not yet implemented commands in elFinder.NetCore
    var disabled = ['callback', 'chmod', 'editor', 'netmount', 'ping', 'search', 'zipdl', 'help'];
    elFinder.prototype.i18.en.messages.TextArea = "Edit";

    $.each(disabled, function (i, cmd) {
        (idx = $.inArray(cmd, myCommands)) !== -1 && myCommands.splice(idx, 1);
    });

    var options = {
        baseUrl: "/lib/elfinder/",
        url: "/el-finder-file-system/connector",
        rememberLastDir: false,
        commands: myCommands,
        lang: 'vi',
        uiOptions: {
            toolbar: [
                ['back', 'forward'],
                ['reload'],
                ['home', 'up'],
                ['mkdir', 'mkfile', 'upload'],
                ['open', 'download'],
                ['undo', 'redo'],
                ['info'],
                ['quicklook'],
                ['copy', 'cut', 'paste'],
                ['rm'],
                ['duplicate', 'rename', 'edit'],
                ['selectall', 'selectnone', 'selectinvert'],
                ['view', 'sort']
            ]
        },
        //onlyMimes: ["image", "text/plain"] // Get files of requested mime types only
        lang: 'vi',
    };
    $('#elfinder').elfinder(options).elfinder('instance');
});
