# umbraco-angularjs-frontend-plugin

Plugin that enables AngularJS for Umbraco frontend.

[![CodeFactor](https://www.codefactor.io/repository/github/sintraconsulting/umbraco-angularjs-frontend-plugin/badge?style=flat-square)](https://www.codefactor.io/repository/github/sintraconsulting/umbraco-angularjs-frontend-plugin)

## Why use this plugin

For intranet or interactive application, using the standard MVC of Umbraco with jQuery has some
limitation. This plugin gives a front-end framework, based on AngularJS, to speed-up development
giving a standard envirnoment.
By installing the plugin is possible to add Angular view\controller to the application.
Using naming convention, binding of view and model is managed automatically. Moreover, it allows to
place single components into pages using standard editor. This plugin helps also to make reusable
code (services, components) for front-end development.

## How it works

By installing the plugin, core DLL are added, this allows to integrate AngularJS into frontend.

### Steps for configuration

1. Install plugin from Umbraco marketplace.
2. The plugin will create a new masterpage and an Angular enabled content page.
3. Unlock Angular page under regular content or home.
4. Add view or component into applications.

Check the image below to see the plugin in action:

![tutorial angular umbraco](https://raw.githubusercontent.com/Sintraconsulting/umbraco-angularjs-frontend-plugin/master/docs/images/usage.gif)

Enable AngularJS on a page:

![enable angular macro on umbraco frontend](https://raw.githubusercontent.com/Sintraconsulting/umbraco-angularjs-frontend-plugin/master/docs/images/enable-angular-umbraco.png)

Add an Angular view into a page:

![add angularview on umbraco frontend](https://raw.githubusercontent.com/Sintraconsulting/umbraco-angularjs-frontend-plugin/master/docs/images/add-angular-view.png)

Configure Angular view on Umbraco, using the editor and this macro:

![configure angular view on umbraco](https://raw.githubusercontent.com/Sintraconsulting/umbraco-angularjs-frontend-plugin/master/docs/images/configure-angular-view.png)

Use conventional placement of file to bind view with controllers:

![conventional folder placement](https://raw.githubusercontent.com/Sintraconsulting/umbraco-angularjs-frontend-plugin/master/docs/images/conventional-folder.png)

### Naming convention and folder structure

TBD

## AngularJs page not visible under content page or home

Just add it as "allowed child" under home, content page, or other content types.
![enable angular js under home of umbraco](https://raw.githubusercontent.com/Sintraconsulting/umbraco-angularjs-frontend-plugin/master/docs/images/enable-angular-umbraco.png)

![enable angular js unde content type page of umbraco](https://raw.githubusercontent.com/Sintraconsulting/umbraco-angularjs-frontend-plugin/master/docs/images/enable-on-content-page.png)

## Reference

- [Plugin page on umbraco markeplace](https://our.umbraco.com/packages/developer-tools/umbraco-angularjs-frontend-plugin/)
