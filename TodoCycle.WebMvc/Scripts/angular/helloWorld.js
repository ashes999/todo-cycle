// app component
(function (app)
{
    app.AppComponent =
      ng.core.Component({
          selector: 'my-app',
          template: '<h1>Hello Angular! 2+3={{2 + 3}}</h1>'
      })
      .Class({
          constructor: function () { console.log("app.component constructed!"); }
      });
})(window.app || (window.app = {}));

// app model
(function (app)
{
    app.AppModule =
      ng.core.NgModule({
          imports: [ng.platformBrowser.BrowserModule],
          declarations: [app.AppComponent],
          bootstrap: [app.AppComponent]
      })
      .Class({
          constructor: function () { console.log("App.module constructed"); }
      });
})(window.app || (window.app = {}));


// main
(function (app)
{
    document.addEventListener('DOMContentLoaded', function ()
    {
        ng.platformBrowserDynamic
          .platformBrowserDynamic()
          .bootstrapModule(app.AppModule);
    });
})(window.app || (window.app = {}));
