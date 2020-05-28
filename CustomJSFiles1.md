dinamik lookbook adını verdiğimiz sanal bir dergilik oluşturan custom bir arge projesi ve çizim yazılımı olan bir projeye ait js dosyasıdır.
örnek olması açısından paylaşılmıştır. ihtiyaç halinde projenin canlı demosu gösterilebilir.



RTG.DRAW.DrawOperations = {

    ready: function () {
        RTG.DRAW.DrawOperations.bindListenersDraw();
    },
    /* Functions of group draw */
    FireGroupMouseDown: function (e) {
        var ctx = canvasData;
        clicked = 1;
        var pos = RTG.DRAW.getXY(e);
        ctx.beginPath();
        xx = pos.x;
        yy = pos.y;
        ctx.moveTo(xx, yy);
        points.push(pos);
        RTG.DRAW.DrawOperations.qwIconsShow(false);
        RTG.DRAW.ExtraIcon.ExtraIconsShow(false);
        RTG.DRAW.VideoElement.VideoElementsShow(false);

    },

    FireGroupMouseMove: function (e) {
        var ctx = canvasData;
        if (clicked) {
            var pos = RTG.DRAW.getXY(e);
            xx = pos.x;
            yy = pos.y;
            ctx.lineTo(xx, yy);
            ctx.stroke();
            points.push(pos);
            last = pos;
        }
    },

    FireGroupMouseUp: function (e) {
        clicked = 0;
        RTG.DRAW.DrawOperations.qwIconsShow(true);
        RTG.DRAW.ExtraIcon.ExtraIconsShow(true);
        RTG.DRAW.VideoElement.VideoElementsShow(true);

        //RTG.DRAW.DrawOperations.GroupMinMax();
    },

    FireGroupTouchMove: function (e) {
        var ctx = canvasData;
        if (clicked) {
            e.preventDefault();
            var pos = RTG.DRAW.getXY(e.targetTouches[0]);
            xx = pos.x;
            yy = pos.y;
            ctx.lineTo(xx, yy);
            ctx.stroke();
            points.push(pos);
            last = pos;
        }
    },

    FireGroupTouchEnd: function (e) {
        clicked = 0;
        RTG.DRAW.DrawOperations.qwIconsShow(true);
        RTG.DRAW.ExtraIcon.ExtraIconsShow(true);
        RTG.DRAW.VideoElement.VideoElementsShow(true);


        //RTG.DRAW.DrawOperations.GroupMinMax();
    },

    FireGroupTouchStart: function (e) {
        e.preventDefault();
        var ctx = canvasData;
        clicked = 1;
        ctx.beginPath();
        var pos = RTG.DRAW.getXY(e.targetTouches[0]);
        xx = pos.x;
        yy = pos.y;
        ctx.moveTo(xx, yy);
        points.push(pos);
        RTG.DRAW.DrawOperations.qwIconsShow(false);
        RTG.DRAW.ExtraIcon.ExtraIconsShow(false);
        RTG.DRAW.VideoElement.VideoElementsShow(false);

    },

    GroupMinMax: function () {
        var ctx = RTG.DRAW.DrawOperations.GetCurrentCanvasData();
        textOfGroup = document.getElementById('groupingtext').value;
        var minX = 1000000,
            minY = 1000000,
            maxX = -1000000,
            maxY = -1000000,
            i = 0, p,
            lw = ctx.lineWidth;
        var width = ctx.canvas.width;
        var height = ctx.canvas.height;
        for (; p = points[i++];) {
            if (p.x > maxX) maxX = p.x;
            if (p.y > maxY) maxY = p.y;
            if (p.x < minX) minX = p.x;
            if (p.y < minY) minY = p.y;
            p.x = (((p.x * 100) / width));
            p.y = (((p.y * 100) / height));
            postData = postData + p.x + "," + p.y + ",";

        }
        var maphilight = "inside";
        var opacity = "opacity";
        var groupSettings = RTG.DRAW.DrawOperations.CreateGroupSettingsForAddOperation();

        var lookBookPGIViewModel = { // LookBookGroupDO
            "LookBookPageID": lookbookPageId,
            "Coordinates": postData,
            "ImageHeight": height,
            "ImageWidth": width,
            "TextOfGroupingGroups": textOfGroup,
            "GroupSettings": RTG.DRAW.DrawOperations.CreateGroupSettingsForAddOperation(),
            "inverseDraw": document.getElementById('inverseDraw').checked,
            //"drawOnClokWise": document.getElementById('drawOnClockwise').checked,

        }

        var lookBookGroupDO = { // LookBookGroupDO
            "Id": 0,
            "LookBookPageId": lookbookPageId,
            "Coordinates": postData,
            "IsActive": true,
            "TextOfGrouping": textOfGroup,
            "GroupSettings": groupSettings,
        }

        swal({
            title: "Do you want to save the group?",
            text: "This process cannot be taken back!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes.",
            cancelButtonText: "No!",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    RTG.DRAW.showLoading(true);
                    $.ajax({
                        url: '/LookBookGroup/Add',
                        contentType: "application/json",
                        method: "post",
                        dataType: "json",
                        data: JSON.stringify(lookBookPGIViewModel),
                        success: function (data) {
                            if (data.data != 0) {
                                swal("Group Drawing Operation", "Group drawing was saved. ", "success");
                                RTG.DRAW.DrawOperations.ChangeDrawToggleStatus(false);
                                isGroupDraw = false;
                                currentGroupID = data.data;
                                document.getElementById('PostID').value = data.data;
                                RTG.DRAW.showLoading(false);
                                window.location.href = '/LookBookPage/DrawOperation2' + '?lookBookGroupId=0&lookBookPageId=' + lookbookPageId;
                            }
                            else {
                                swal("Group Drawing Operation", "Group drawing could not be saved!", "error");
                                ctx.drawImage(previousCanvasData, 0, 0, ctx.canvas.width, ctx.canvas.height);
                                RTG.DRAW.showLoading(false);
                            }
                        }

                    });

                } else {
                    swal("Group Drawing Operation", "Group drawing was cancelled. ", "error");
                    ctx.drawImage(previousCanvasData, 0, 0, ctx.canvas.width, ctx.canvas.height);
                    RTG.DRAW.showLoading(false);
                }
                RTG.DRAW.DrawOperations.ResetGroupParameters();
                canvasData = RTG.DRAW.DrawOperations.GetCurrentCanvasData();
                lastPointDisconnectContinueDraw = [-100, -100];
            });

    },

    /* Functions of group draw */
    FireItemMouseDown: function (e) {
        if (currentGroupID !== null && currentGroupID !== undefined && currentGroupID !== "" && currentGroupID !== 0) {
            var ctx = canvasData;
            if (e.which == RTG.MouseClickEnums.LEFT_CLICK) {
                clicked = 1;
                ctx.beginPath();
                var pos = RTG.DRAW.getXY(e);
                xx = pos.x;
                yy = pos.y;
                ctx.moveTo(xx, yy);
                points.push(pos);
                RTG.DRAW.DrawOperations.qwIconsShow(false);
                RTG.DRAW.ExtraIcon.ExtraIconsShow(false);
                RTG.DRAW.VideoElement.VideoElementsShow(false);

            }
            else if (e.which == RTG.MouseClickEnums.RIGHT_CLICK && document.getElementById('lbPGItemID').value != 0) {
                RTG.DRAW.DrawOperations.qwIconsShow(true);
                RTG.DRAW.ExtraIcon.ExtraIconsShow(true);
                RTG.DRAW.VideoElement.VideoElementsShow(true);

                var pos = RTG.DRAW.getXY(e);
                var canvas = document.getElementById('canvas');
                var width = canvas.width;
                var height = canvas.height;
                var x = ((((pos.x - 34) * 100) / width)) + '%';
                var y = ((((pos.y - 34) * 100) / height)) + '%';
                var num;
                var numCopy = [];
                var result = 0;
                num = Math.floor(Math.random() * 100) + 1;
                var elements = document.getElementsByClassName("quickWiewCheck");
                if (elements != null && elements.length != 0) {

                    for (var i = 0; i < elements.length; i++) {
                        var name = elements[i].getAttribute('name');
                        numCopy.push(name);
                    }
                    while (result != -1) {
                        num = Math.floor(Math.random() * 100) + 1;
                        result = RTG.DRAW.checkUniqueDivNumber(numCopy, num);
                    }
                }
                else {
                    num = Math.floor(Math.random() * 100) + 1;
                }
                console.log(x, y);
                var div = $('<div id="quickWiewDiv' + num + '" class="quickWiewCheck" value="0" name="' + num + '">').css({
                    "position": "absolute",
                    "left": x,
                    "top": y,
                    "background-image": "url('../icons/quick-view_icon.png')",
                    "background-size": "cover",
                    "background-repeat": "no-repeat",
                    "bacground-color": "red",
                    "margin": "0",
                    "padding": "0",
                    "width": '68',
                    "height": '68',
                    "z-index": '999',
                    "opacity": '1'
                })
                $('.canvasDiv').append(div);
                RTG.DRAW.callDraggable("quickWiewDiv" + num + "", y, x);
                SaveQuickViewIconForItem("quickWiewDiv" + num + "");
            }
            else {
                sweetAlert("Oops...", "Firstly, you must draw an item.", "error");
            }
        }
        else {
            sweetAlert("Oops...", "Firstly, you must draw a group.", "error");
        }

    },

    FireItemMouseMove: function (e) {
        var ctx = canvasData;
        if (clicked) {
            var pos = RTG.DRAW.getXY(e);
            xx = pos.x;
            yy = pos.y;
            ctx.lineTo(xx, yy);
            ctx.stroke();
            points.push(pos);
            last = pos;
        }
    },

    FireItemMouseUp: function (e) {
        clicked = 0;
        RTG.DRAW.DrawOperations.qwIconsShow(true);
        RTG.DRAW.ExtraIcon.ExtraIconsShow(true);
        RTG.DRAW.VideoElement.VideoElementsShow(true);

        //RTG.DRAW.DrawOperations.ItemMinMax();
    },

    FireItemTouchMove: function (e) {
        var ctx = canvasData;
        if (clicked) {
            e.preventDefault();
            var pos = RTG.DRAW.getXY(e.targetTouches[0]);
            xx = pos.x;
            yy = pos.y;
            ctx.lineTo(xx, yy);
            ctx.stroke();
            points.push(pos);
            last = pos;
        }
    },

    FireItemTouchStart: function (e) {
        if (currentGroupID !== null && currentGroupID !== undefined && currentGroupID !== "" && currentGroupID !== 0) {
            var ctx = canvasData;
            e.preventDefault();
            if (e.type == 'touchstart') {

                clicked = 1;
                ctx.beginPath();
                var pos = RTG.DRAW.getXY(e.targetTouches[0]);
                xx = pos.x;
                yy = pos.y;
                ctx.moveTo(xx, yy);
                points.push(pos);
                RTG.DRAW.DrawOperations.qwIconsShow(false);
                RTG.DRAW.ExtraIcon.ExtraIconsShow(false);
                RTG.DRAW.VideoElement.VideoElementsShow(false);


            }
            else if (e.type == 'touchstart' && times > 1) {
                RTG.DRAW.DrawOperations.qwIconsShow(true);
                RTG.DRAW.ExtraIcon.ExtraIconsShow(true);
                RTG.DRAW.VideoElement.VideoElementsShow(true);

                var pos = RTG.DRAW.getXY(e.targetTouches[0]);
                var canvas = document.getElementById('canvas');
                var width = canvas.width;
                var height = canvas.height;
                var x = ((((pos.x - 34) * 100) / width)) + '%';
                var y = ((((pos.y - 34) * 100) / height)) + '%';
                var num;
                var numCopy = [];
                var result = 0;
                num = Math.floor(Math.random() * 100) + 1;
                var elements = document.getElementsByClassName("quickWiewCheck");
                if (elements != null && elements.length != 0) {
                    for (var i = 0; i < elements.length; i++) {
                        var name = elements[i].getAttribute('name');
                        numCopy.push(name);
                    }
                    while (result != -1) {
                        num = Math.floor(Math.random() * 100) + 1;
                        result = RTG.DRAW.checkUniqueDivNumber(numCopy, num);
                    }
                }
                else {
                    num = Math.floor(Math.random() * 100) + 1;
                }
                var div = $('<div id="quickWiewDiv' + num + '" class="quickWiewCheck" value="0" name="' + num + '">').css({
                    "position": "absolute",
                    "left": x,
                    "top": y,
                    "background-image": "url('../icons/quick-view_icon.png')",
                    "background-size": "cover",
                    "background-repeat": "no-repeat",
                    "bacground-color": "red",
                    "margin": "0",
                    "padding": "0",
                    "width": '68',
                    "height": '68',
                    "z-index": '999',
                    "opacity": '1'
                })
                $('.canvasDiv').append(div);
                RTG.DRAW.callDraggable("quickWiewDiv" + num + "", y, x);
                SaveQuickViewIconForItem("quickWiewDiv" + num + "");
            }
            else {
                sweetAlert("Oops...", "Firstly, you must draw an item.", "error");
            }
        }
        else {
            sweetAlert("Oops...", "Firstly, you must draw an group.", "error");
        }

    },

    FireItemTouchEnd: function (e) {
        clicked = 0;
        RTG.DRAW.DrawOperations.qwIconsShow(true);
        RTG.DRAW.ExtraIcon.ExtraIconsShow(true);
        RTG.DRAW.VideoElement.VideoElementsShow(true);

        //RTG.DRAW.DrawOperations.ItemMinMax();
    },

    ItemMinMax: function () {
        var ctx = RTG.DRAW.DrawOperations.GetCurrentCanvasData();
        textOfGroupItem = document.getElementById('groupingtext').value;
        {
            var minX = 1000000,
                minY = 1000000,
                maxX = -1000000,
                maxY = -1000000,
                i = 0, p,
                lw = ctx.lineWidth;
            var postData = "";
            var width = ctx.canvas.width;
            var height = ctx.canvas.height;
            for (; p = points[i++];) {
                if (p.x > maxX) maxX = p.x;
                if (p.y > maxY) maxY = p.y;
                if (p.x < minX) minX = p.x;
                if (p.y < minY) minY = p.y;
                defX = defX + p.x;
                defY = defY + p.y;
                p.x = (((p.x * 100) / width));
                p.y = (((p.y * 100) / height));
                postData = postData + p.x + "," + p.y + ",";
                countOfCoord++;
            }
            var gID = document.getElementById('PostID').value;
            if (currentGroupID != null || currentGroupID !== undefined || currentGroupID == 0) {
                gID = currentGroupID
            }
            if (gID == 0) {
                sweetAlert("Oops...", "You must first create a group!", "error");
                ctx.drawImage(previousCanvasData, 0, 0, ctx.canvas.width, ctx.canvas.height)
                RTG.DRAW.DrawOperations.ResetItemParameters();
                canvasData = RTG.DRAW.DrawOperations.GetCurrentCanvasData();
            }
            else {

                swal({
                    title: "Do you want to save the item?",
                    text: "This process can not be taken back!",
                    type: "warning",
                    showCancelButton: true,
                    confirmButtonColor: "#DD6B55",
                    confirmButtonText: "Save",
                    cancelButtonText: "Cancel",
                    closeOnConfirm: false,
                    closeOnCancel: false
                },
                    function (isConfirm) {
                        var valGroupID = gID;
                        defY = defY / countOfCoord;
                        defX = defX / countOfCoord;
                        defY = ((((defY - 34) * 100) / canvas.height)) + '%';
                        defX = ((((defX - 34) * 100) / canvas.width)) + '%';
                        var lookBookGroupItemDO = {
                            "Id": 0,
                            "GroupId": gID,
                            "Coordinates": postData,
                            "IsActive": true,
                            "QuickWievCoords": defY + "," + defX,
                            "IsDefaultQuickWievIcon": true,
                            "TextOfGrouping": textOfGroupItem,
                        }
                        if (isConfirm) {
                            RTG.DRAW.showLoading(true);
                            $.ajax({
                                url: '/LookBookGroupItem/Add',
                                method: "post",
                                dataType: "json",
                                contentType: "application/json",
                                data: JSON.stringify(lookBookGroupItemDO),
                                success: function (data) {
                                    if (data.data != 0) {
                                        console.log(data.data);
                                        swal("Item Draw Operation", "Item draving was saved.", "success");
                                        document.getElementById('lbPGItemID').value = data.data;
                                        console.log(data.data.groupId, lookbookPageId, data.data.quickWievCoords);
                                        RTG.DRAW.DrawOperations.getDrawOperation(data.data.groupId, lookbookPageId, data.data.quickWievCoords, data.data.id);
                                        RTG.DRAW.showLoading(false);
                                    }
                                    else {
                                        swal("Item Draw Operation", " Item draving could not be saved!", "error");
                                        ctx.drawImage(previousCanvasData, 0, 0, ctx.canvas.width, ctx.canvas.height);
                                        RTG.DRAW.showLoading(false);
                                    }
                                }
                            });

                        } else {
                            swal("Item Draw Operation", "Item draving was cancelled.", "error");
                            ctx.drawImage(previousCanvasData, 0, 0, ctx.canvas.width, ctx.canvas.height);
                            RTG.DRAW.showLoading(false);
                        }
                        RTG.DRAW.DrawOperations.ResetItemParameters();
                        canvasData = RTG.DRAW.DrawOperations.GetCurrentCanvasData();
                        lastPointDisconnectContinueDraw = [-100, -100];
                    });
            }
        }
    },

    ResetGroupParameters: function () {
        points = [];
        last;
        clicked = 0;
        postData = "";
        canvasData = null;
        previousCanvasIsShouldSet = true;
    },

    ResetItemParameters: function () {
        points = [];
        last;
        clicked = 0;
        defX = 0;
        defY = 0;
        countOfCoord = 0;
        postData = "";
        canvasData = null;
        previousCanvasIsShouldSet = true;
    },

    DrawItemForGroup: function (groupID) {
        RTG.DRAW.selectOperationType('click-draw');
        RTG.DRAW.DrawOperations.ChangeDrawToggleStatus(false);
        swal({
            title: "Add new item",
            text: "Now, You can add an item to your selected group",
            timer: 500,
            showConfirmButton: false
        });
        isGroupDraw = false;
        currentGroupID = groupID;
        textOfGroupItem = "";
    },

    deleteGroup: function (groupID) {
        swal({
            title: "Do you want to delete group?",
            text: "This process can not be taken back!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Clean",
            cancelButtonText: "Cancel",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                RTG.DRAW.showLoading(true);
                if (isConfirm) {
                    $.ajax({
                        url: '/LookBookGroup/DeleteGroup',
                        method: "post",
                        dataType: "json",
                        data: { 'deleteGroupByID': groupID, 'deleteAll': 'True' },
                        success: function (data) {
                            if (data.isSuccess == true) {
                                swal("Delete Group Operation", "Group has been deleted.", "success");
                                window.location.href = '/LookBookPage/DrawOperation2' + '?lookBookGroupId=0&lookBookPageId=' + lookbookPageId;
                                RTG.DRAW.showLoading(false);
                            }
                            else {
                                swal("Delete Group Operation", "Group could not be deleted!", "error");
                                RTG.DRAW.showLoading(false);
                            }
                        }
                    });

                } else {
                    swal("Delete Group Operation", "Deletion has been cancelled.", "error");
                    RTG.DRAW.showLoading(false);
                }
            });
    },

    deleteGroupItems: function (groupID) {
        swal({
            title: "Do you want to clean up the items of the group?",
            text: "This process can not be taken back!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes, Clean",
            cancelButtonText: "Cancel",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    RTG.DRAW.showLoading(true);
                    $.ajax({

                        url: '/LookBookGroup/DeleteGroup',
                        method: "post",
                        dataType: "json",
                        data: { 'deleteGroupByID': groupID, 'deleteAll': 'False' },
                        success: function (data) {
                            if (data.isSuccess == true) {
                                swal("Cleaning operation was successful.", "Items of the group were cleaned.", "success");
                                window.location.href = '/LookBookPage/DrawOperation2' + '?lookBookGroupId=' + groupID + '&lookBookPageId=' + lookbookPageId;
                                RTG.DRAW.showLoading(false);
                            }
                            else {
                                swal("Cancel", "Cleaning operation was not successfull!", "error");
                                RTG.DRAW.showLoading(false);
                            }
                        }
                    });

                } else {
                    swal("Cancel", "Cleaning operation was not successfull!", "error");
                    RTG.DRAW.showLoading(false);
                }
            });
    },

    editAlert: function (groupID, TextOfGrouping) {
        openedLeftMenuId = "groupEdit";
        openedRightMenuId = "right-bar-group-edit-options";
        RTG.DRAW.showLoading(true);
        currentGroupID = groupID;
        operationType = RTG.OperationEnums.GROUP_EDIT;

        var clickedLi = $('#groupEdit');
        $("#left-bar-ul").parent().find('li').removeClass("active");
        clickedLi.addClass('active');
        $('#draw').fadeOut();
        $('#groupEdit').fadeIn('slow');
        $('#right-bar-group-edit-options').fadeIn();
        $('#extra-icons').fadeOut();
        $('#right-bar-extra-icon').fadeOut();
        $('#video-element').fadeOut();
        $('#right-bar-video-element').fadeOut();
        $('#lookbookpage-settings').fadeOut();
        $("#groupingtext-div").show();
        $("#groupingtext").show();

        $("#inverse-group-list li").removeClass("active");
        $("#inverse-group-list li").removeClass("hidden");
        //var array = RTG.DRAW.DrawOperations.GetGroupSettings(groupID);
        //console.log('2' + array);
        RTG.DRAW.DrawOperations.CreateEditOptions(groupID);
        RTG.DRAW.showLoading(false);

    },

    CreateEditOptions: function (groupID) {
        $("#" + groupID + "-inverse-group-li").addClass("hidden");
        //$('#flashing-effect-group-settings').bootstrapSwitch('onText', "in");
        //$('#flashing-effect-group-settings').bootstrapSwitch('offText', "out");
        //$('#flashing-effect-group-settings').bootstrapSwitch('state', true);

        //$('#flashing-effect-group-settings').bootstrapSwitch({
        //    onSwitchChange: function (e, state) {
        //        RTG.DRAW.ExtraIcon.SetExtraIconCssValuesWithTextbox(this.id, true);
        //    }
        //});

        //var drawfill = array.filter('fill');
        //console.log(drawfill);

        $.ajax({
            url: '/LookBookGroup/GetGroupSettings',
            method: "post",
            dataType: "json",
            data: { 'groupID': groupID },
            success: function (data) {
                if (data.isSuccess) {
                    $('#groupingtext-edit').val(data.data.textOfGrouping);
                    if (data.data.groupSettings != 0) {
                        for (i = 0; i < data.data.groupSettings.length; i++) {
                            $('#draw' + data.data.groupSettings[i].key).val(data.data.groupSettings[i].value);
                        }
                        RTG.DRAW.showLoading(false);
                        array = data.data.groupSettings;

                        var inverseGroupId = RTG.DRAW.Find(array, 'key', 'inverse-group');
                        if (inverseGroupId !== null && inverseGroupId !== undefined && inverseGroupId !== '') {
                            $('#' + inverseGroupId.value + '-inverse-group-li').addClass('active');
                        }
                        //Color 
                        var strokeColor = RTG.DRAW.Find(array, 'key', 'strokeColor').value;
                        var fillColor = RTG.DRAW.Find(array, 'key', 'fillColor').value;
                        var shadowColor = RTG.DRAW.Find(array, 'key', 'shadowColor').value;
                        $('#drawstrokeColor').minicolors('value', strokeColor);
                        $('#drawfillColor').minicolors('value', fillColor);
                        $('#drawshadowColor').minicolors('value', shadowColor);


                        var fillEffect = (RTG.DRAW.Find(array, 'key', 'fill').value == 'true');
                        var fadeEffect = (RTG.DRAW.Find(array, 'key', 'fade').value == 'true');
                        var alwaysOnEffect = (RTG.DRAW.Find(array, 'key', 'alwaysOn').value == 'true');
                        var neveronEffect = (RTG.DRAW.Find(array, 'key', 'neverOn').value == 'true');
                        var groupby = (RTG.DRAW.Find(array, 'key', 'groupBy').value == 'true');
                        var wrapClass = (RTG.DRAW.Find(array, 'key', 'wrapClass').value == 'true');
                        var stroke = (RTG.DRAW.Find(array, 'key', 'stroke').value == 'true');
                        var shadow = (RTG.DRAW.Find(array, 'key', 'shadow').value == 'true');
                        var shadowPosition = (RTG.DRAW.Find(array, 'key', 'shadowPosition').value == 'true');


                        $('#drawfill').bootstrapSwitch('state', fillEffect);
                        $('#drawfade').bootstrapSwitch('state', fadeEffect);
                        $('#drawalwayson').bootstrapSwitch('state', alwaysOnEffect);
                        $('#drawneveron').bootstrapSwitch('state', neveronEffect);
                        $('#drawgroupby').bootstrapSwitch('state', groupby);
                        $('#drawwrapClass').bootstrapSwitch('state', wrapClass);
                        $('#drawstroke').bootstrapSwitch('state', stroke);
                        $('#drawshadow').bootstrapSwitch('state', shadow);
                        $('#drawshadowPosition').bootstrapSwitch('state', shadowPosition);

                        $('#drawfill').bootstrapSwitch({
                            onSwitchChange: function (e, state) {
                                RTG.DRAW.ExtraIcon.SetExtraIconCssValuesWithTextbox(this.id, true);
                            }
                        });

                        $('#drawfade').bootstrapSwitch({
                            onSwitchChange: function (e, state) {
                                RTG.DRAW.ExtraIcon.SetExtraIconCssValuesWithTextbox(this.id, true);
                            }
                        });

                        $('#drawalwayson').bootstrapSwitch({
                            onSwitchChange: function (e, state) {
                                RTG.DRAW.ExtraIcon.SetExtraIconCssValuesWithTextbox(this.id, false);
                            }
                        });
                        $('#drawneveron').bootstrapSwitch({
                            onSwitchChange: function (e, state) {
                                RTG.DRAW.ExtraIcon.SetExtraIconCssValuesWithTextbox(this.id, true);
                            }
                        });
                        $('#drawgroupby').bootstrapSwitch({
                            onSwitchChange: function (e, state) {
                                RTG.DRAW.ExtraIcon.SetExtraIconCssValuesWithTextbox(this.id, true);
                            }
                        });
                        $('#drawwrapClass').bootstrapSwitch({
                            onSwitchChange: function (e, state) {
                                RTG.DRAW.ExtraIcon.SetExtraIconCssValuesWithTextbox(this.id, true);
                            }
                        });

                        $('#drawstroke').bootstrapSwitch({
                            onSwitchChange: function (e, state) {
                                RTG.DRAW.ExtraIcon.SetExtraIconCssValuesWithTextbox(this.id, true);
                            }
                        });

                        $('#drawshadow').bootstrapSwitch({
                            onSwitchChange: function (e, state) {
                                RTG.DRAW.ExtraIcon.SetExtraIconCssValuesWithTextbox(this.id, true);
                            }
                        });

                        $('#drawshadowPosition').bootstrapSwitch({
                            onSwitchChange: function (e, state) {
                                RTG.DRAW.ExtraIcon.SetExtraIconCssValuesWithTextbox(this.id, true);
                            }
                        });

                        operationType = RTG.OperationEnums.GROUP_EDIT;
                    }
                }
                else {
                    swal("Group Settings Operation", "Group settings could not be found!", "error");
                    ctx.drawImage(previousCanvasData, 0, 0, ctx.canvas.width, ctx.canvas.height);
                    RTG.DRAW.showLoading(false);
                }
            }
        });

    },

    quickWiewDraggable: function (paramID) {
        $("#" + paramID).draggable({
            start: function () {
            },
            drag: function (e) {
                var div = document.getElementById(paramID);
                var pos = RTG.DRAW.getXY(e);
                var canvas = document.getElementById('canvas');
                var width = canvas.width;
                var height = canvas.height;
                div.style.position = 'absolute';
                div.style.top = ((((pos.y - 34) * 100) / height)) + '%';
                div.style.left = ((((pos.x - 34) * 100) / width)) + '%';
            },
            stop: function (e) {
                var div = document.getElementById(paramID);
                var pos = RTG.DRAW.getXY(e);
                var canvas = document.getElementById('canvas');
                var width = canvas.width;
                var height = canvas.height;

                div.style.top = ((((pos.y - 34) * 100) / height)) + '%';
                div.style.left = ((((pos.x - 34) * 100) / width)) + '%';
                RTG.DRAW.callDropable();
            }
        }

        )
    },

    SaveQuickViewIconForItem: function (divID) {
        var valItemID = document.getElementById('lbPGItemID').value;
        var div = document.getElementById(divID);
        var qwItemID = document.getElementById(divID).getAttribute('value');
        var postCoords = div.style.top + "," + div.style.left;
        console.log(postCoords)
        var externalCode = "";
        if (qwItemID != null) {
            RTG.DRAW.showLoading(true);
            $.ajax({
                url: '/LookBookGroupItem/GetExternalCodeForEdit',
                method: "post",
                dataType: "json",
                data: { 'lookbookGroupItemID': qwItemID },
                success: function (data) {
                    externalCode = data;
                    RTG.DRAW.showLoading(false);
                    swal({
                        title: "Releated Item",
                        text: "Enter the external code which is associated with the item",
                        type: "input",
                        confirmButtonText: 'Save',
                        cancelButtonText: 'Cancel',
                        showCancelButton: false,
                        closeOnConfirm: false,
                        animation: "slide-from-top",
                        inputPlaceholder: "Item External Code",
                        inputValue: qwItemID == 0 ? "" : externalCode,
                        allowOutsideClick: true
                    },
                        function (inputValue) {
                            if (inputValue === false) return false;

                            if (inputValue === "") {
                                swal.showInputError("Item external code cannot be empty!");
                                return false
                            }
                            $.ajax({
                                url: '/LookBookGroupItem/InsertSearchIcon',
                                method: "post",
                                dataType: "json",
                                data: { 'iconXandYCoordinates': postCoords, 'itemID': valItemID, "isActive": 'true', 'quickWiewItemID': qwItemID, 'productExternalCode': inputValue },
                                success: function (data) {
                                    if (data.data == -1) {
                                        swal.showInputError("Invalid Item External Code!");
                                    }
                                    else if (data.data == -2) {
                                        document.getElementById(divID).style.display = "none";
                                        swal({
                                            title: "Info",
                                            text: "Item has already had a quick watch coordinate",
                                            timer: 3000,
                                            showConfirmButton: true
                                        });
                                    }
                                    else if (data.data != 0) {
                                        swal({
                                            title: "Successfull",
                                            text: "Quick Watch Coordinates were associated with the item.",
                                            timer: 2000,
                                            showConfirmButton: false
                                        });

                                        document.getElementById(divID).setAttribute("value", data.data);
                                    }
                                    else {
                                        swal({
                                            title: "UnSuccessfull",
                                            text: "Quick Watch Coordinates are not associated with the item.!",
                                            timer: 2000,
                                            showConfirmButton: false
                                        });
                                    }
                                }
                            });
                        }
                    );
                }
            });
        }
    },

    qwIconsShow: function (param) {
        var elementsIcon = document.getElementsByClassName("quickWiewCheck");
        var elementsGroup = document.getElementsByClassName("groupEdit");
        if (elementsIcon != null && elementsIcon.length != 0) {
            if (param == true) {
                for (var i = 0; i < elementsIcon.length; i++) {
                    elementsIcon[i].style.display = "block";
                }
            }
            else {
                for (var i = 0; i < elementsIcon.length; i++) {
                    elementsIcon[i].style.display = "none";
                }
            }
        }
        if (elementsGroup != null && elementsGroup.length != 0) {
            if (param == true) {
                for (var i = 0; i < elementsGroup.length; i++) {
                    elementsGroup[i].style.display = "block";
                }
            }
            else {
                for (var i = 0; i < elementsGroup.length; i++) {
                    elementsGroup[i].style.display = "none";
                }
            }
        }
    },

    GetCurrentCanvasData: function () {
        var ctx = canvas.getContext('2d'),
            img = new Image;
        imgPivot = new Image;
        imgPivot.src = canvas.toDataURL();
        if (isGroupDraw == true) {
            ctx.strokeStyle = '	#7FFFD4';
            ctx.lineCap = 'round';
            ctx.lineWidth = 2;
        }
        else {
            ctx.lineCap = 'round';
            ctx.strokeStyle = '	#CD5C5C';
            ctx.lineWidth = 2;
        }
        img.src = canvas.toDataURL();
        return ctx;
    },

    PreviousCanvasData: function () {

        var ctx = canvas.getContext('2d'),
            img = new Image;
        imgPivot = new Image;
        imgPivot.src = canvas.toDataURL();
        if (isGroupDraw == true) {
            ctx.strokeStyle = '	#7FFFD4';
            ctx.lineCap = 'round';
            ctx.lineWidth = 2;
        }
        else {
            ctx.lineCap = 'round';
            ctx.strokeStyle = '	#CD5C5C';
            ctx.lineWidth = 2;
        }
        img.src = canvas.toDataURL();
        return imgPivot;
    },

    getDrawOperation: function (groupID, LookBookPageId, coordinates, id) {
        $.ajax({
            url: '/LookBookPage/DrawOperation',
            method: "Get",
            data: { lookBookGroupId: groupID, lookBookPageId: LookBookPageId },

            success: function (data) {
                RTG.DRAW.DrawOperations.qwIconsShow(true);
                RTG.DRAW.ExtraIcon.ExtraIconsShow(true);
                RTG.DRAW.VideoElement.VideoElementsShow(true);

                var IconCoordinates = coordinates;
                var num;
                var numCopy = [];
                var result = 0;
                num = Math.floor(Math.random() * 100) + 1;
                var elements = document.getElementsByClassName("quickWiewCheck");
                if (elements != null && elements.length != 0) {
                    for (var i = 0; i < elements.length; i++) {
                        var name = elements[i].getAttribute('name');
                        numCopy.push(name);
                    }
                    while (result != -1) {
                        num = Math.floor(Math.random() * 100) + 1;
                        result = RTG.DRAW.checkUniqueDivNumber(numCopy, num);
                    }
                }
                else {
                    num = Math.floor(Math.random() * 100) + 1;
                }
                var IconY = IconCoordinates.split(',')[0];
                var IconX = IconCoordinates.split(',')[1];
                var top = IconY.replace("%", "");
                var left = IconX.replace("%", "");
                var div = "";
                top = IconY.replace("%", "");
                left = IconX.replace("%", "");

                div = $('<div id="quickWiewDiv' + num + '" data-btn-type="4" class="quickWiewCheck" ondblclick="RTG.DRAW.DoubleClick(quickWiewDiv' + num + ',' + id + ',' + top + ',' + left + ')"  value="' + id + '" name="' + num + '">').css({
                    "position": "absolute",
                    "left": IconX,
                    "top": IconY,
                    "background-image": "url('../icons/errorQwIcon.png')",
                    "background-size": "cover",
                    "background-repeat": "no-repeat",
                    "bacground-color": "red",
                    "margin": "0",
                    "padding": "0",
                    "width": '68',
                    "height": '68',
                    "opacity": '1'
                })
                $('.canvasDiv').append(div);

                RTG.DRAW.DrawOperations.quickWiewDraggable("quickWiewDiv" + num);
                RTG.DRAW.callDropable();
                RTG.DRAW.HoldOn("quickWiewDiv" + num, id, top, left);
                top = 0;
                left = 0;
            }
        });
    },

    DrawGroupWithGivenCoordinates: function () {

        var canvas = document.getElementById('canvas');
        var context = canvas.getContext('2d');

        var image = new Image();   // using optional size for image
        image.onload = drawImageActualSize; // draw when image has loaded
        image.crossOrigin = "Anonymous";
        var ctx = RTG.DRAW.DrawOperations.GetCurrentCanvasData();

        image.src = imageUrl;



        function drawImageActualSize() {

            var ctx1 = RTG.DRAW.DrawOperations.GetCurrentCanvasData();
            var regex = '[0-9.,]';
            var coordinates = document.getElementById('coordinate-input').value;
            var coordinatesData = coordinates.match(regex) != null ? coordinates.match(regex).input : null;
            if (coordinatesData == null || coordinatesData.length < 1) {
                ctx1.drawImage(returnOfCoordinateCanvasData, 0, 0, image.width * sizerOfNeed, image.height * sizerOfNeed);

                return;
            }
            else {

                var coordinates = coordinatesData.split(',');
                var count = Math.round(coordinates.length / 2);
                //  returnOfCoordinateCanvasData = RTG.DRAW.DrawOperations.PreviousCanvasData();
                for (i = 0; i < count; i++) {
                    context.beginPath();
                    context.rect(coordinates[(i * 2)] * sizerOfNeed, (coordinates[(i * 2) + 1] * sizerOfNeed), 1, 1);
                    context.fillStyle = 'red';
                    context.fill();
                    context.lineWidth = 7;
                    context.strokeStyle = 'red';
                    context.stroke();
                }
            }
        }
    },

    drawImageWithCanvasSize: function (img) {
        return img;
    },

    SaveGroupAndItemWithGivenProduct: function () {
        var coordinatesOfSplit = document.getElementById('coordinate-input').value;
        var image = new Image();   // using optional size for image
        image.onload = RTG.DRAW.DrawOperations.drawImageWithCanvasSize(image); // draw when image has loaded
        image.crossOrigin = "Anonymous";
        // load an image of intrinsic size 300x227 in CSS pixels
        image.src = imageUrl;
        var ctx = RTG.DRAW.DrawOperations.GetCurrentCanvasData();


        var sizer = RTG.DRAW.DrawOperations.drawImageWithCanvasSize(image);

        var width = image.width;
        var height = image.height;
        var c = document.getElementById("canvas");
        var ctx1 = c.getContext("2d");
        textOfGroup = document.getElementById('groupingtext').value;


        var lookBookPGIViewModel = { // LookBookGroupDO
            "LookBookPageID": lookbookPageId,
            "Coordinates": coordinatesOfSplit,
            "imageHeight": height,
            "imagewidth": width,
            "LookBookGroupID": currentGroupID,
            "TextOfGroupingGroups": textOfGroup,
            "inverseDraw": document.getElementById('inverseDraw').checked,
            "GroupSettings": RTG.DRAW.DrawOperations.CreateGroupSettingsForAddOperation()

        }

        if (isGroupDraw == true && currentGroupID < 1) {
            textOfGroup = document.getElementById('groupingtext').value;
            swal({
                title: "Do you want to save the group?",
                text: "This process cannot be taken back!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes.",
                cancelButtonText: "No!",
                closeOnConfirm: false,
                closeOnCancel: false
            },
                function (isConfirm) {
                    if (isConfirm) {
                        RTG.DRAW.showLoading(true);
                        $.ajax({
                            url: '/LookBookGroup/CreateGroupWithTakenCoordsFromOutside',
                            contentType: "application/json",
                            method: "post",
                            dataType: "json",
                            data: JSON.stringify(lookBookPGIViewModel),
                            success: function (data) {
                                if (data.data != 0) {
                                    swal("Group Drawing Operation", "Group drawing was saved. ", "success");
                                    RTG.DRAW.DrawOperations.ChangeDrawToggleStatus(false);
                                    isGroupDraw = false;
                                    currentGroupID = data.data;
                                    document.getElementById('PostID').value = data.data;
                                    RTG.DRAW.showLoading(false);
                                    window.location.reload();
                                }
                                else {
                                    swal("Group Drawing Operation", "Group drawing could not be saved!", "error");
                                    //ctx.drawImage(previousCanvasData, 0, 0, ctx.canvas.width, ctx.canvas.height);
                                    RTG.DRAW.showLoading(false);
                                }
                            }

                        });

                    } else {
                        swal("Group Drawing Operation", "Group drawing was cancelled. ", "error");
                        ctx1.drawImage(returnOfCoordinateCanvasData, 0, 0, ctx.canvas.width, ctx.canvas.height);
                        RTG.DRAW.showLoading(false);
                    }
                    RTG.DRAW.DrawOperations.ResetGroupParameters();
                    canvasData = RTG.DRAW.DrawOperations.GetCurrentCanvasData();
                });

        }

        //Item Drawing
        else {
            var gID = document.getElementById('PostID').value;
            textOfGroupItem = document.getElementById('groupingtext').value;
            if (currentGroupID != null || currentGroupID !== undefined || currentGroupID == 0) {
                gID = currentGroupID
            }
            if (gID == 0) {
                sweetAlert("Oops...", "You must first create a group!", "error");
                ctx.drawImage(previousCanvasData, 0, 0, ctx.canvas.width, ctx.canvas.height)
                RTG.DRAW.DrawOperations.ResetItemParameters();
                canvasData = RTG.DRAW.DrawOperations.GetCurrentCanvasData();
            }

            var points = coordinatesOfSplit.split(',');
            var count = Math.round(points.length / 2);
            var minX = 1000000,
                minY = 1000000,
                maxX = -1000000,
                maxY = -1000000;

            var postData = '';
            defX = 0;
            defY = 0;
            for (var i = 0; i < count; i++) {

                if (parseInt(points[(i * 2)]) > maxX) maxX = parseInt(points[(i * 2)]);
                if (parseInt(points[(i * 2) + 1]) > maxY) maxY = parseInt(points[(i * 2) + 1]);
                if (parseInt(points[(i * 2)]) < minX) minX = parseInt(points[(i * 2)]);
                if (parseInt(points[(i * 2) + 1]) < minY) minY = parseInt(points[(i * 2) + 1]);


                defX = defX + parseInt(points[(i * 2)]);
                defY = defY + parseInt(points[(i * 2) + 1]);


                points[(i * 2)] = (((parseInt(points[(i * 2)]) * 100) / width));
                points[(i * 2) + 1] = (((parseInt(points[(i * 2) + 1]) * 100) / height));

                postData = postData + points[(i * 2)] + "," + points[(i * 2) + 1] + ",";
            }
            defY = defY * sizerOfNeed / count;
            defX = defX * sizerOfNeed / count;
            defY = ((((defY - 34) * 100) / canvas.height)) + '%';
            defX = ((((defX - 34) * 100) / canvas.width)) + '%';
            var LookBookPGIViewModel = {
                "LookBookGroupID": gID,
                "Coordinates": coordinatesOfSplit,
                "IsActive": true,
                "QuickViewCoordinates": defY + "," + defX,
                "imageHeight": height,
                "imagewidth": width,
                "IsDefaultQuickWievIcon": true,
                "TextOfGroupingItems": textOfGroupItem,

            };

            swal({
                title: "Do you want to save the Item for Group?",
                text: "This process cannot be taken back!",
                type: "warning",
                showCancelButton: true,
                confirmButtonColor: "#DD6B55",
                confirmButtonText: "Yes.",
                cancelButtonText: "No!",
                closeOnConfirm: false,
                closeOnCancel: false
            },
                function (isConfirm) {
                    if (isConfirm) {
                        RTG.DRAW.showLoading(true);
                        $.ajax({
                            url: '/LookBookGroupItem/CreateItempWithTakenCoordsFromOutside',
                            contentType: "application/json",
                            method: "post",
                            dataType: "json",
                            data: JSON.stringify(LookBookPGIViewModel),
                            success: function (data) {
                                if (data.data != 0) {
                                    swal("Item Drawing Operation", "Item drawing was saved. ", "success");
                                    RTG.DRAW.DrawOperations.ChangeDrawToggleStatus(false);
                                    isGroupDraw = false;
                                    currentGroupID = data.data;
                                    document.getElementById('PostID').value = data.data;
                                    RTG.DRAW.showLoading(false);
                                    window.location.reload();
                                }
                                else {
                                    swal("Item Drawing Operation", "Item drawing could not be saved!", "error");
                                    ctx.drawImage(previousCanvasData, 0, 0, ctx.canvas.width, ctx.canvas.height);
                                    RTG.DRAW.showLoading(false);
                                }
                            }

                        });

                    } else {
                        swal("Item Drawing Operation", "Item drawing was cancelled. ", "error");
                        ctx1.drawImage(returnOfCoordinateCanvasData, 0, 0, ctx.canvas.width, ctx.canvas.height);
                        RTG.DRAW.showLoading(false);
                    }
                    RTG.DRAW.DrawOperations.ResetGroupParameters();
                    canvasData = RTG.DRAW.DrawOperations.GetCurrentCanvasData();
                });

        }
    },

    SaveGroupAndItemDisconneContinue: function () {
        if (isGroupDraw == true) {
            clicked = 0;
            RTG.DRAW.DrawOperations.qwIconsShow(true);
            RTG.DRAW.ExtraIcon.ExtraIconsShow(true);
            RTG.DRAW.VideoElement.VideoElementsShow(true);

            RTG.DRAW.DrawOperations.GroupMinMax();
        }
        else {
            clicked = 0;
            RTG.DRAW.DrawOperations.qwIconsShow(true);
            RTG.DRAW.ExtraIcon.ExtraIconsShow(true);
            RTG.DRAW.VideoElement.VideoElementsShow(true);

            RTG.DRAW.DrawOperations.ItemMinMax();
        }
    },

    saveGroupSettings: function () {
        var groupSettings = RTG.DRAW.DrawOperations.CreateGroupSettingsForUpdateOperation();
        var text = document.getElementById('groupingtext-edit').value;
        var lookbookGroupDO = { // lookbookGroupDO
            "Id": currentGroupID,
            "LookBookPageId": lookbookPageId,
            "IsActive": true,
            "TextOfGrouping": text,
            "GroupSettings": groupSettings
        }

        swal({
            title: "Do you want to save group settings ?",
            text: "This process cannot be taken back!",
            type: "warning",
            showCancelButton: true,
            confirmButtonColor: "#DD6B55",
            confirmButtonText: "Yes.",
            cancelButtonText: "No!",
            closeOnConfirm: false,
            closeOnCancel: false
        },
            function (isConfirm) {
                if (isConfirm) {
                    RTG.DRAW.showLoading(true);
                    $.ajax({
                        url: '/LookBookGroup/SaveGroupSettings',
                        contentType: "application/json",
                        method: "post",
                        dataType: "json",
                        data: JSON.stringify(lookbookGroupDO),
                        success: function (data) {
                            if (data.isSuccess) {
                                swal("Group Settings", "Settings are saved. ", "success");
                                RTG.DRAW.showLoading(false);
                            }
                            else {

                                swal("Group Settings", "Settings could not be saved!", "error");
                                RTG.DRAW.showLoading(false);
                            }
                        }
                    });

                }
                else {

                    swal("Group Settings", "An operation of save group settings was cancelled. ", "error");
                    RTG.DRAW.showLoading(false);
                }
            });
    },

    bindListenersDraw: function () {

        $('#operationTypeSelect').on('change', function (e) {
            var optionSelected = $("option:selected", this);
            var valueSelected = this.value;
            previousCanvasIsShouldSet = true;
            //   $('#drawOperationType').val(valueSelected);
            drawOperationType = valueSelected;
            if (valueSelected == RTG.DrawOperationEnums.ONE_TIME_DRAW) {
                $("#drawGroupAndItems").show();
                $("#coordinate-div").hide();
                $("#inverse-draw-div").show();
                $("#inversepopover").show();
                $("#groupingtext-div").show();
                $('#coordinate-input').val('');
                RTG.DRAW.DrawOperations.DrawGroupWithGivenCoordinates();

            }
            if (valueSelected == RTG.DrawOperationEnums.DISCONNE_CONTINUE_DRAW) {
                $("#drawGroupAndItems").show();
                $("#groupingtext-div").show();
                $("#inverse-draw-div").show();
                $("#inversepopover").show();
                $("#coordinate-div").hide();
                $('#coordinate-input').val('');
                RTG.DRAW.DrawOperations.DrawGroupWithGivenCoordinates();


            }
            else if (valueSelected == RTG.DrawOperationEnums.IMPORT_COORDINATES_DRAW) {
                $("#coordinate-div").show();
                $("#groupingtext-div").show();
                $("#inverse-draw-div").show();
                $("#inversepopover").show();
                $("#drawGroupAndItems").show();
                $('#coordinate-input').val('');

            }
        });
        RTG.DRAW.DrawOperations.GenerateInverseGroupList();
    },

    /* continue draw functions */
    FireDrawContinueGroupMouseDown: function (e) {
        previousCanvasIsShouldSet = false;
        var ctx = canvasData;
        clicked = 1;
        var pos = RTG.DRAW.getXY(e);
        console.log(pos);
        ctx.beginPath();
        xx = pos.x;
        yy = pos.y;
        if (lastPointDisconnectContinueDraw[0] != -100 && lastPointDisconnectContinueDraw[1] != -100) {
            ctx.moveTo(lastPointDisconnectContinueDraw[0], lastPointDisconnectContinueDraw[1]);
        }
        else {
            ctx.moveTo(xx, yy);
        }
        ctx.lineTo(xx, yy);
        ctx.stroke();
        points.push(pos);
        lastPointDisconnectContinueDraw[0] = xx;
        lastPointDisconnectContinueDraw[1] = yy;
    },

    FireDrawContinueItemMouseDown: function (e) {
        if (currentGroupID !== null && currentGroupID !== undefined && currentGroupID !== "" && currentGroupID !== 0) {
            previousCanvasIsShouldSet = false;
            var ctx = canvasData;
            clicked = 1;
            var pos = RTG.DRAW.getXY(e);
            console.log(pos);
            ctx.beginPath();
            xx = pos.x;
            yy = pos.y;
            if (lastPointDisconnectContinueDraw[0] != -100 && lastPointDisconnectContinueDraw[1] != -100) {
                ctx.moveTo(lastPointDisconnectContinueDraw[0], lastPointDisconnectContinueDraw[1]);
            }
            else {
                ctx.moveTo(xx, yy);
            }
            ctx.lineTo(xx, yy);
            ctx.stroke();
            points.push(pos);
            lastPointDisconnectContinueDraw[0] = xx;
            lastPointDisconnectContinueDraw[1] = yy;
        }
        else {
            sweetAlert("Oops...", "Firstly, you must draw an group.", "error");
        }

    },

    FireDrawContinueGroupTouchStart: function (e) {
        e.preventDefault();
        var ctx = canvasData;
        clicked = 1;
        ctx.beginPath();
        var pos = RTG.DRAW.getXY(e.targetTouches[0]);
        xx = pos.x;
        yy = pos.y;

        if (lastPointDisconnectContinueDraw[0] != -100 && lastPointDisconnectContinueDraw[1] != -100) {
            ctx.moveTo(lastPointDisconnectContinueDraw[0], lastPointDisconnectContinueDraw[1]);
        }
        else {
            ctx.moveTo(xx, yy);
        }
        ctx.lineTo(xx, yy);
        ctx.stroke();
        points.push(pos);
        lastPointDisconnectContinueDraw[0] = xx;
        lastPointDisconnectContinueDraw[1] = yy;
    },

    FireDrawContinueItemTouchStart: function (e) {
        if (currentGroupID !== null && currentGroupID !== undefined && currentGroupID !== "" && currentGroupID !== 0) {
            e.preventDefault();
            var ctx = canvasData;
            clicked = 1;
            ctx.beginPath();
            var pos = RTG.DRAW.getXY(e.targetTouches[0]);
            xx = pos.x;
            yy = pos.y;

            if (lastPointDisconnectContinueDraw[0] != -100 && lastPointDisconnectContinueDraw[1] != -100) {
                ctx.moveTo(lastPointDisconnectContinueDraw[0], lastPointDisconnectContinueDraw[1]);
            }
            else {
                ctx.moveTo(xx, yy);
            }
            ctx.lineTo(xx, yy);
            ctx.stroke();
            points.push(pos);
            lastPointDisconnectContinueDraw[0] = xx;
            lastPointDisconnectContinueDraw[1] = yy;
        }
        else {
            sweetAlert("Oops...", "Firstly, you must draw an group.", "error");
        }

    },


    CreateGroupSettingsForAddOperation: function () {
        var data = [
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "fill",
                "Value": "true",
            },

            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "fillColor",
                "Value": "000000",
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "fillOpacity",
                "Value": "0.2",
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "stroke",
                "Value": true,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "strokeColor",
                "Value": "ff0000",
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "strokeOpacity",
                "Value": "1",
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "opacity",
                "Value": "1",
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "strokeWidth",
                "Value": "1",
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "fade",
                "Value": true,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "alwaysOn",
                "Value": false,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "neverOn",
                "Value": false,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "groupBy",
                "Value": true,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "wrapClass",
                "Value": true,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadow",
                "Value": false,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadowX",
                "Value": "0",
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadowY",
                "Value": "0",
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadowRadius",
                "Value": "6",
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadowColor",
                "Value": "000000",
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadowOpacity",
                "Value": "0.8",
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadowPosition",
                "Value": false,
            },

            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadowFrom",
                "Value": false,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "inverse-group",
                "Value": '',
            },
        ]

        return data;
    },

    CreateGroupSettingsForUpdateOperation: function () {

        var drawfill = document.getElementById('drawfill').checked;
        var drawfillOpacity = document.getElementById('drawfillOpacity').value;
        var drawfillColor = document.getElementById('drawfillColor').value.replace('#', '');
        var drawfade = document.getElementById('drawfade').checked;
        var drawalwayson = document.getElementById('drawalwayson').checked;
        var drawneveron = document.getElementById('drawneveron').checked;
        var drawgroupby = document.getElementById('drawgroupby').checked;
        var drawwrapClass = document.getElementById('drawwrapClass').checked;
        var drawstroke = document.getElementById('drawstroke').checked;
        var drawstrokeOpacity = document.getElementById('drawstrokeOpacity').value;
        var drawstrokeColor = document.getElementById('drawstrokeColor').value.replace('#', '');
        var drawstrokeWidth = document.getElementById('drawstrokeWidth').value;
        var drawshadow = document.getElementById('drawshadow').checked;
        var drawshadowOpacity = document.getElementById('drawshadowOpacity').value;
        var drawshadowX = document.getElementById('drawshadowX').value;
        var drawshadowY = document.getElementById('drawshadowY').value;
        var drawshadowColor = document.getElementById('drawshadowColor').value.replace('#', '');
        var drawshadowPosition = document.getElementById('drawshadowPosition').checked;
        var drawshadowRadius = document.getElementById('drawshadowRadius').value;
        var selectedIconLiId = $('ul#inverse-group-list').find('li.active').attr('id');
        var selectedLi = document.getElementById(selectedIconLiId);
        var inverseGroupId = "";
        if (selectedLi !== null && selectedLi !== undefined && selectedLi !== '') {
            var inverseGroupId = selectedLi.getAttribute('data-group-id');
        }


        var data = [
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "fill",
                "Value": drawfill,
            },

            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "fillColor",
                "Value": drawfillColor,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "fillOpacity",
                "Value": drawfillOpacity,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "stroke",
                "Value": drawstroke,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "strokeColor",
                "Value": drawstrokeColor,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "strokeOpacity",
                "Value": drawstrokeOpacity,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "opacity",
                "Value": "1",
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "strokeWidth",
                "Value": drawstrokeWidth,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "fade",
                "Value": drawfade,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "alwaysOn",
                "Value": drawalwayson,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "neverOn",
                "Value": drawneveron,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "groupBy",
                "Value": drawgroupby,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "wrapClass",
                "Value": drawwrapClass,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadow",
                "Value": drawshadow,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadowX",
                "Value": drawshadowX,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadowY",
                "Value": drawshadowY,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadowRadius",
                "Value": drawshadowRadius,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadowColor",
                "Value": drawshadowColor,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadowOpacity",
                "Value": drawshadowOpacity,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "shadowPosition",
                "Value": drawshadowPosition,
            },
            {
                "GroupID": currentGroupID,
                "LookBookPageId": lookbookPageId,
                "Key": "inverse-group",
                "Value": inverseGroupId,
            },

        ]

        return data;
    },

    DrawSingleGroupForInverseUlLi: function (coordinates) {
        var canvasPivot = document.createElement('canvas');
        var ctxPivot = canvasPivot.getContext("2d");

        var img1 = new Image();
        img1.crossOrigin = "Anonymous";
        img1.src = imageUrl;
        img1.onload = function () {
            var w = img1.width;
            var h = img1.height;
            canvasPivot.width = '140';
            canvasPivot.height = '140';
            canvasPivot.style.marginLeft = '-11px';
            canvasPivot.style.marginTop = '-6px';
            ctxPivot.drawImage(img1, 0, 0, w, h, 0, 0, 140, 140);
            var lastX;
            var lastY;
            var posX;
            var posY;
            var width;
            var height;
            var deleteX;
            var deleteY;
            var count = (coordinates.split(',') || []).length;
            count = Math.round(count / 2);
            ctxPivot.strokeStyle = '	#7FFFD4';
            ctxPivot.lineCap = 'round';
            ctxPivot.lineWidth = 2;
            for (var i = 1; i <= count; i++) {

                width = ctxPivot.canvas.width;
                height = ctxPivot.canvas.height;
                lastX = coordinates.split(',')[0];
                deleteX = lastX;
                lastX = (RTG.DRAW.Divide((lastX * width), 100));
                lastY = coordinates.split(',')[1];
                deleteY = lastY;
                lastY = (RTG.DRAW.Divide((lastY * height), 100))
                posX = coordinates.split(',')[2];
                posX = (RTG.DRAW.Divide((posX * width), 100))
                posY = coordinates.split(',')[3];
                posY = (RTG.DRAW.Divide((posY * height), 100))
                ctxPivot.beginPath();
                ctxPivot.moveTo(lastX, lastY);
                ctxPivot.lineTo(posX, posY);
                ctxPivot.stroke();
                coordinates = coordinates.replace(deleteX + ',', '');
                coordinates = coordinates.replace(deleteY + ',', '');
            }
        }

        return canvasPivot;
    },

    GenerateInverseGroupList: function () {

        var ul = document.getElementById('inverse-group-list');
        ul.innerHTML = '';
        groupList.forEach(function (item) {
            var li = document.createElement('li');
            li.classList.add('list-group-item');
            li.classList.add('custom-margin');
            li.style.width = '150px';
            li.style.height = '150px';
            li.setAttribute('id', '' + item.id + '-inverse-group-li');
            li.setAttribute('data-group-id', item.id);
            li.onclick = function (e) {
                e.preventDefault();
                $(this).toggleClass('active').siblings().removeClass("active");
            };
            var createCanvas = RTG.DRAW.DrawOperations.DrawSingleGroupForInverseUlLi(item.coordinates);
            li.appendChild(createCanvas);
            ul.appendChild(li);
        });
    },

    ChangeDrawToggleStatus: function (status) {
        if (status) {
            $('.toggle').addClass('on');
            $('#inverse-draw-div').removeClass('hidden');
            $("#inversepopover").show();

        }
        else {
            $('.toggle').addClass('off');
            $('#inverse-draw-div').addClass('hidden');
            $("#inversepopover").addClass('hidden')

        }
    }
}
