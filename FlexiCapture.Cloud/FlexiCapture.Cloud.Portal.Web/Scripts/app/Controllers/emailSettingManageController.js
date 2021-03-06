﻿var emailSettingManageController = function ($scope, $http, $location, $state, $uibModal, $uibModalInstance, $stateParams, usSpinnerService, items) {
    var vm = this;
    
    function loadSettingData() {
        vm.$scope = $scope;
        vm.items = items;
        vm.user = $scope.user;
        $scope.submitted = false;
        vm.confirm = function () {
            if ($scope.settingForm.$valid) {
                $scope.submitted = false;
                $uibModalInstance.close();
            }
            else { $scope.submitted = true; }
        }
        vm.cancel = $uibModalInstance.dismiss;
    }
    loadSettingData();

};



fccApp.controller("emailSettingManageController", ["$scope", "$http", "$location", "$state", "$uibModal", "$stateParams", "usSpinnerService", ftpSettingManageController]);
