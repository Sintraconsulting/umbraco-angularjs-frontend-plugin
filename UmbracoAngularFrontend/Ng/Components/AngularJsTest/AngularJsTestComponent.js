'use strict';

function AngularJsTestController($scope, TestService) {
    var _self = this;

    _self.textFromComponent = 'This text is from the component';
    _self.textFromService = '';

    _self.$onInit = function () {
        _self.textFromService = TestService.getText();
    }
}

var AngularJsTestComponent = {
    templateUrl: '/Ng/Components/AngularJsTest/AngularJsTestComponent.html',
    controllerAs: 'tc',
    controller: AngularJsTestController
};