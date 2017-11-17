tinymce.PluginManager.add('drive', function (editor, url) {
    // Add a button that opens a window
    editor.addButton('drive', {
        text: 'Drive',
        icon: false,
        onclick: function () {
            // Open window
            editor.windowManager.open({
                title: 'drive plugin',
                body: [
                    { type: 'textbox', name: 'title', label: 'Title' }
                ],
               
                onsubmit: function (e) {
                    // Insert content when the window form is submitted
                    editor.insertContent('<div><iframe src="https://drive.google.com/embeddedfolderview?id=1WjYs7yqsVR4A5hx8AkLwe41I0fwnEXOa#list" style="width:100%; height:100px; border:0;"></iframe></div>');
                }
            });
        }
    });

    // Adds a menu item to the tools menu
    editor.addMenuItem('drive', {
        text: 'Drive',
        context: 'tools',
        onclick: function () {
            // Open window with a specific url
            editor.windowManager.open({
                title: 'drive plugin',
                body: [
                    { type: 'textbox', name: 'title', label: 'Title' }
                ],
               
                onsubmit: function (e) {
                    // Insert content when the window form is submitted
                    editor.insertContent('<div><iframe src="https://drive.google.com/embeddedfolderview?id=1WjYs7yqsVR4A5hx8AkLwe41I0fwnEXOa#list" style="width:100%; height:100px; border:0;"></iframe></div>');
                }
            });
        }
    });

    return {
        getMetadata: function () {
            return {
                title: "drive plugin",
                url: "http://exampleplugindocsurl.com"
            };
        }
    };
});