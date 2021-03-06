﻿$("button#bumenadd").click(function () {

    layer.prompt({ title: '输入部门名称', formType: 0 }, function (pass, index) {

        $.getJSON("/ajax/bumen/add", { name: pass }, function (data) {
            layer.close(index);
            layer.msg(data.msg);

            reload();
        });


    });

});
$.getJSON("/ajax/bumen", {}, function (json) {
    var data = json.data;
    for (var i in data) {
        var jsonObj = data[i];

        $("#star").append("<option value='" + jsonObj.id + "'>" + jsonObj.name + "</option>");
    }
});
layui.use('table', function () {
    var table = layui.table;

    table.render({
        elem: '#test3'
        , url: '/ajax/bumen/'
        , cellMinWidth: 80 //全局定义常规单元格的最小宽度，layui 2.2.1 新增
        , cols: [[
            { type: 'checkbox', fixed: 'left' }
            , { field: 'id', width: 100, title: 'ID', sort: true }
            , { field: 'name', width: 350, title: '部门名称', edit: 'text' }
            , { field: 'seqno', width: 80, title: '排序', edit: 'text' }
            , { field: 'remark', title: '备注说明', edit: 'text' }

        ]]
    });

    //监听单元格编辑
    table.on('edit(test3)', function (obj) {
        var value = obj.value //得到修改后的值
            , data = obj.data //得到所在行所有键值
            , field = obj.field; //得到字段

        // layer.msg('[ID: ' + data.id + '] ' + field + ' 字段更改为：' + value);
        if (data.id == "1515131137") {
            layer.msg("超级管理员为系统设定,不能修改");
            return;
        }
        $.getJSON("/ajax/fieldupdate", { table: "bumen", id: data.id, key: field, value: value }, function (json) {
            layer.msg(json.msg);
            reload();
        });

    });

    var $ = layui.$, active = {
        getCheckData: function () {

            layer.confirm('您确定要删除吗？', {
                btn: ['Yes', 'No'] //按钮
            }, function () {

                //获取选中数据
                var checkStatus = table.checkStatus('test3')
                    , data = checkStatus.data;
                //layer.alert(JSON.stringify(data));
                var ids = [];
                for (var a in data) {
                    if (data[a].id == "1515131137") {
                        layer.msg("超级管理员为系统设定,不能删除,请重新勾选");
                        return;
                    }
                    ids.push(data[a].id);
                }
                console.log(ids.join());
                $.getJSON("/ajax/fieldupdate", { table: "bumen", id: ids.join(), key: "status", value: -1 }, function (json) {
                    layer.msg(json.msg);
                    reload();
                });


            }, function () {

            });

        }
        , getCheckLength: function () { //获取选中数目
            var checkStatus = table.checkStatus('test3')
                , data = checkStatus.data;
            layer.msg('选中了：' + data.length + ' 个');
        }
        , isAll: function () { //验证是否全选
            var checkStatus = table.checkStatus('test3');
            layer.msg(checkStatus.isAll ? '全选' : '未全选');
        }
    };

    $('.demoTable .layui-btn').on('click', function () {
        var type = $(this).data('type');
        active[type] ? active[type].call(this) : '';
    });

});