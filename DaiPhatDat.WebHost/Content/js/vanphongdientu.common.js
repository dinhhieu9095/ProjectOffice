/**
 * các function js dùng chung
 * */
var vanPhongDienTuCommon = {
    /**
     * khởi tạo cây menu
     * 
     * */
    initJstree: function (param) {
        $(param.id).jstree({
            'core': {
                'data': {
                    "url": function (node) {
                        return param.getUrl(node);
                    },
                    'type': 'GET',
                    'dataType': 'JSON',
                    'contentType': 'application/json;',
                    'async': 'false',
                    'data': function (node) {
                        
                    }
                },
                "themes": {
                    "responsive": false
                },
                'check_callback': true,
            },
            "plugins": ["themes", "ui"]
        }).bind('ready.jstree', function (event, data) {
            if (param.currentNodeId != undefined && param.currentNodeId != "") {
                $(param.id).jstree(true).select_node(param.currentNodeId);
            }
            else {
                if ($(param.id).jstree().get_selected(true)[0] != null && $(param.id).jstree().get_selected(true)[0] != undefined) {
                    param.currentNodeId = $(param.id).jstree().get_selected(true)[0].id;
                }
            }
            param.selectNode();
        }).bind("select_node.jstree", function (event, data) {
            param.currentNodeId = $(param.id).jstree().get_selected(true)[0].id;
            param.selectNode();
        });
    },
    initJstreeCheckBox: function (param) {
        $(param.treeId).jstree({
            'core': {
                'data': {
                    "url": function (node) {
                        return param.getUrl(node);
                    },
                    'type': 'GET',
                    'dataType': 'JSON',
                    'contentType': 'application/json;',
                    'async': 'true',
                    'data': function (node) {

                    }
                },
                "themes": {
                    "responsive": false
                },
                'check_callback': true,
            },
            "search": {
                "show_only_matches": true,
                "case_sensitive": false
            },
            "checkbox": {
                "keep_selected_style": false
            },
            "plugins": ["themes", "ui", "checkbox", "search", "dnd"]
        }).bind('ready.jstree', function (event, data) {
            
        }).bind("select_node.jstree", function (event, data) {
           
        });
        $(param.submitId).on("click", function () {
            var checked_ids = [];
            var tree = $(param.treeId).jstree(true);
            if (tree != null && tree != undefined) {
                if ($(param.treeId).jstree(true) !== false) {
                    var datas = $(param.treeId).jstree(true).get_json('#', { flat: true });
                    var checkednode = $(param.treeId).jstree("get_checked", null, true);
                    if (checkednode !== undefined && checkednode.length > 0) {
                        var nodes = datas.filter(e => checkednode.indexOf(e.id) != -1);

                        nodes.forEach
                            (function (item) {
                                if (item.li_attr.type == 'user') {
                                    var node = {
                                        text: item.li_attr.text,
                                        code: item.li_attr.code,
                                        type: item.li_attr.type,
                                        jobTitle: item.li_attr.jobTitle,
                                        department: item.li_attr.department,
                                        id: item.li_attr.id
                                    }
                                } else {
                                    var node = {
                                        text: item.li_attr.text,
                                        code: item.li_attr.code,
                                        type: item.li_attr.type,
                                        id: item.li_attr.id
                                    }
                                }
                                
                                checked_ids.push(node);
                            });
                    }
                    param.submitOrgChart(checked_ids);
                    $('#org-chart-modal-body').html('<div id="org-user-chart-modal"></div>');
                    $(param.submitId).off("click");
                    $(param.modalId).modal('hide');
                }
            }
        });
        $('.org-chart-close-id').on("click", function () {
            $('#org-chart-modal-body').html('<div id="org-user-chart-modal"></div >');
            $(param.submitId).off("click");
        });
        var to = false;
        $('#org-chart-modal-search').keyup(function () {
            if (to) { clearTimeout(to); }
            to = setTimeout(function () {
                var v = $('#org-chart-modal-search').val();
                $(param.treeId).jstree('search',v);
            }, 250);
        });
        $(param.modalId).modal('show');
    },
    reloadJstree: function (param) {
        var jsTree = $(param.id).jstree(true);
        jsTree.settings.core.data = {
            "url": function (node) {
                return param.getUrl(node);
            },
            'type': 'GET',
            'dataType': 'JSON',
            'contentType': 'application/json;',
            'async': 'false',
            'data': function (node) {

            }
        };
        jsTree.refresh();
    },
    //renderJsTree: function (node, callback, param) {
        
    //    callback.call(this, param.callback(node));
    //}
};