'use strict';

//Create app module
var app = angular.module('notesApp', ['ngAnimate', 'ui.bootstrap']);

//Base URL for the REST Service
var baseUrl = '/';

app.factory('NotesService', ['$http', 
    function ($http) {
        var service = {};

        //Call to REST service to retrieve paginated notes  
        service.GetNotes = function (paging) {
            return $http.get(baseUrl + 'api/notes', { params: paging });
        };
        //Call to REST service to add a new note
        service.PostNotes = function (formdata) {
            return $http.post(baseUrl + 'api/notes', formdata);
        };

        return service;
    }])

app.controller('NotesController',
    ['$scope', 'NotesService',
    function ($scope, NotesService) {

        //Object with default values for notes list pagination
        $scope.paging = {
            PageNumber: 1,
            PageSize: 10,
            SortBy: 'Date',
            SortDirection: false,
            TotalCount: 0
        };

        //load call to retrieve notes from rest service 
        function loadNotes() {
            $scope.spinner = "spinner";
            $scope.users = null;
            NotesService.GetNotes($scope.paging).success(function (data) {
                //successfull call
                $scope.notes = data.Results;
                $scope.paging.TotalCount = data.TotalCount;
                $scope.spinner = "";
            }).error(function (data) {
                //error call
                $scope.errormessage = data == null ? "Impossible to reach the server" : data.MessageDetail != undefined ? data.MessageDetail : "Generic error";
                $scope.errortype = "alert-danger";
                $scope.spinner = "";
            });
        }

        // Call to function to call GET service to retrieve paginated notes
        loadNotes();

        //manage errors message div on page
        $scope.errormessage = "";

        //table sort functionality
        $scope.sort = function (sortBy) {
            if (sortBy === $scope.paging.SortBy) {
                $scope.paging.SortDirection = !$scope.paging.SortDirection;
            } else {
                $scope.paging.SortBy = sortBy;
                $scope.paging.SortDirection = false;
            }
            $scope.paging.PageNumber = 1;
            loadNotes();
        };

        //pagination current page
        $scope.setPage = function (pageNo) {
            $scope.paging.PageNumber = pageNo;
        };

        //pagination page changed event of pagination
        $scope.pageChanged = function () {
            loadNotes();
        };


        // create a blank object to hold the create note form information
        $scope.formData = {};
        $scope.editor = { add: false };

        //show form to add new note
        $scope.addnew = function () {
            $scope.formData = {};
            $scope.editor.add = true;
           
        };

        //process the form submit of a new note
        $scope.processAddNewForm = function () {
            $scope.spinner = "spinner";
            //call POST service of the REST service
            NotesService.PostNotes($scope.formData)
            .success(function (data) {
                //success note is added
                $scope.errormessage = "Note added succesfully";
                $scope.errortype = "alert-success";
                $scope.formData = {};
                $scope.editor.add = false;
                loadNotes();
               
            }).
            error(function (data) {
                //error note is not added
                $scope.errormessage = data == null ? "Impossible to reach the server" : data.ModelState["note.Description"] != undefined ? data.ModelState["note.Description"][0] :  data.Message != undefined ? data.Message : "API Error";
                $scope.errortype = "alert-danger";
                $scope.spinner = "";
            });
        };

    }]);