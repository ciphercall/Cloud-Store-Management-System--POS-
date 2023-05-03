var ChequePaymentApp = angular.module("ChequePaymentApp", ['ui.bootstrap']);

ChequePaymentApp.controller("ApiChequePaymentController", function ($scope, $http) {

    $scope.loading = false;
    $scope.addMode = false;


    $scope.search = function (event) {

        $scope.loading = true;
        event.preventDefault();

        var compid = $('#txtcompid').val();
        var transDate = $('#txt_Date').val();
        var transType = $('#txt_Type option:selected').val();

        $http.get('/api/ApiChequePayment/GetPaymentData/', {
            params: {
                cid: compid,
                date: transDate,
                type: transType
            }
        }).success(function (data) {
            if (data[0].count == 1) {
                $scope.PaymentData = null;
            } else {
                $scope.PaymentData = data;
            }
            $scope.loading = false;
        });

    };




    $scope.toggleEdit = function () {
        this.testitem.editMode = !this.testitem.editMode;
    };



    $scope.toggleEdit_Cancel = function () {
        this.testitem.editMode = !this.testitem.editMode;

        //Grid view load 
        var compid = $('#txtcompid').val();
        var transDate = $('#txt_Date').val();
        var transType = $('#txt_Type option:selected').val();

        $http.get('/api/ApiChequePayment/GetPaymentData/', {
            params: {
                cid: compid,
                date: transDate,
                type: transType
            }
        }).success(function (data) {
            $scope.PaymentData = data;
            $scope.loading = false;
        });
    };




    //Update to grid level data (save a record after edit)
    $scope.update = function () {
        $scope.loading = true;
        var frien = this.testitem;

        this.testitem.COMPID = $('#txtcompid').val();
        this.testitem.INSUSERID = $('#txtInsertUserid').val();
        this.testitem.INSLTUDE = $('#latlon').val();
        this.testitem.TransDate = $('#txt_Date').val();
        this.testitem.TRANSTP = $('#txt_Type option:selected').val();
        var Update = $('#txt_Updateid').val();

        if (Update == "I") {
            alert("Permission Denied!!");
            frien.editMode = false;

            //Grid view load 
            var compid = $('#txtcompid').val();
            var transDate = $('#txt_Date').val();
            var transType = $('#txt_Type option:selected').val();

            $http.get('/api/ApiChequePayment/GetPaymentData/', {
                params: {
                    cid: compid,
                    date: transDate,
                    type: transType
                }
            }).success(function (data) {
                $scope.PaymentData = data;
                $scope.loading = false;
            });

            $scope.loading = false;
        }
        else {
            $http.post('/api/ApiChequePayment/UpdateData', this.testitem).success(function (data) {
                if (data.MEDIID != 0) {
                    alert("Saved Successfully!!");
                } else {
                    alert("Duplicate data will not create!");
                }

                frien.editMode = false;

                //Grid view load 
                var compid = $('#txtcompid').val();
                var transDate = $('#txt_Date').val();
                var transType = $('#txt_Type option:selected').val();

                $http.get('/api/ApiChequePayment/GetPaymentData/', {
                    params: {
                        cid: compid,
                        date: transDate,
                        type: transType
                    }
                }).success(function (data) {
                    $scope.PaymentData = data;
                });
                $scope.loading = false;

            }).error(function (data) {
                $scope.error = "An Error has occured while Saving Friend! " + data;
                $scope.loading = false;

            });
        }
    };




    //print grid level data.
    $scope.print = function () {
        $scope.loading = true;
        
        var element = this;
        var id = this.testitem.Id;

        var cid = $('#txtcompid').val();
        var transDate = $('#txt_Date').val();
        var type = $('#txt_Type option:selected').val();
        var debitcd = this.testitem.DEBITCD;
        window.open('/ChequePayment/ChequePrint?dt=' + transDate + '&d=' + id + '&tp=' + type);
        //$.ajax({
        //    url: "/ChequePayment/ChequePrint",
        //    type: "POST",
        //    data: JSON.stringify({ 'Options': someData }),
        //    dataType: "json",
        //    traditional: true,
        //    contentType: "application/json; charset=utf-8",
        //    success: function(data) {

        //        // perform your save call here

        //        if (data.status == "Success") {
        //            alert("Done");
        //        } else {
        //            alert("Error occurs on the Database level!");
        //        }
        //    },
        //    error: function() {
        //        alert("An error has occured!!!");
        //    }
        //});
        //$.ajax
        //({
        //    url: '@Url.Action("chequePayment", "ChequePayment")',
        //    type: 'post',
        //    data: { compid: cid, date: transDate, transType: type, debit: debitcd },
        //    success: function (data1) {

        //    }
        //});
    };


});