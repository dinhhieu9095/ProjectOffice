app.controller("BaseCtrl", function ($scope) {
    /*
   * element: element want to block if null all page will blocked
   */
    $scope.showLoading = function (element) {
        var data = $(window).data();
        if (data['blockUI.isBlocked'] != null && data['blockUI.isBlocked'] == 1)
            return;

        var messages = `<div class="lds-css ng-scope"><div style="width:100%;height:100%" class="lds-eclipse"><div></div></div><style type="text/css">@keyframes lds-eclipse {
  0% {
    -webkit-transform: rotate(0deg);
    transform: rotate(0deg);
  }
  50% {
    -webkit-transform: rotate(180deg);
    transform: rotate(180deg);
  }
  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
  }
}
@-webkit-keyframes lds-eclipse {
  0% {
    -webkit-transform: rotate(0deg);
    transform: rotate(0deg);
  }
  50% {
    -webkit-transform: rotate(180deg);
    transform: rotate(180deg);
  }
  100% {
    -webkit-transform: rotate(360deg);
    transform: rotate(360deg);
  }
}
.lds-eclipse {
  position: relative;
}
.lds-eclipse div {
  position: absolute;
  -webkit-animation: lds-eclipse 1s linear infinite;
  animation: lds-eclipse 1s linear infinite;
  width: 160px;
  height: 160px;
  top: 20px;
  left: 20px;
  border-radius: 50%;
  box-shadow: 0 4px 0 0 #1c4595;
  -webkit-transform-origin: 80px 82px;
  transform-origin: 80px 82px;
}
.lds-eclipse {
  width: 200px !important;
  height: 200px !important;
  -webkit-transform: translate(-100px, -100px) scale(1) translate(100px, 100px);
  transform: translate(-100px, -100px) scale(1) translate(100px, 100px);
}
</style></div>`;
        if (element == null) {
            $.blockUI({
                css: {
                    backgroundColor: 'none',
                    color: 'none',
                    border: 'none',
                    left: '45%'
                },
                message: messages
            });
        }
        else {
            $(element).block({ message: messages });
        }
    }
    /*
     * remove all loading  
     */
    $scope.hideLoading = function () {
        var data = $(window).data();
        if (data['blockUI.isBlocked'] != null && data['blockUI.isBlocked'] == 0)
            return;
        $.unblockUI();
    }
    $scope.showConfirm = function (message, callback) {
        bootbox.confirm({
            message: message,
            title: "Xác nhận",
            size: 'small',
            className: "small-confirm",
            buttons: {
                confirm: {
                    label: 'Đồng ý',
                    className: 'btn-danger'
                },
                cancel: {
                    label: 'Hủy',
                    className: 'btn-success pull-right'
                }
            },
            callback: callback
        });
    }
});
