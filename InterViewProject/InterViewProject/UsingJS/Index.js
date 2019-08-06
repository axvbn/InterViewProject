$(document).ready(function () {
    function Query() {
        $.ajax({
            type: 'POST', url: '/Home/Index_Query', async: true, dataType: 'json',
            success: function (data) {
                if (data !== null && data !== '') {
                    let rtData = JSON.parse(data);
                    let i = 0;
                    $.each(rtData, function () {
                        let tbody = '<tr><td>' + rtData[i].CategoryID + '</td>' +
                            '<td>' + rtData[i].CategoryName + '</td>' +
                            '<td>' + rtData[i].Description + '</td>' +
                            '<td><input type="button" id="btnEdit" class="btn btn-info" value="編輯"/></td>' +
                            '<td><input type="button" id="btnDelete" class="btn btn-danger" value="刪除"/></td>' +
                            '</tr>';
                        $('#CategoriesTB tbody').append(tbody);
                        i++;
                    })
                }
            }
        })
    }

    function Insert() {
        let objParams = {
            'CategoryID': $('#Modal_CategoryID').val(),
            'CategoryName': $('#Modal_CategoryName').val(),
            'Description': $('#Modal_Description').val()
        };
        $.ajax({
            type: 'POST', url: '/Home/Index_Insert', async: true,
            data: { jData: JSON.stringify(objParams) },
            success: function (data) {
                if (data !== null && data !== '') {
                    alert(data);
                    Query();
                }
            },
            error: function (data) {
                alert(data);
            }
        })
    }

    $('#btnQuery').click(function () {
        $("#CategoriesTB  tr:not(:first)").empty("");
        Query()
    })

    $('#btnInsert').click(function () {
        $('#Modal_CategoryID').attr('readonly', true);
        $('#Modal_btnINSERT').css('display', '');
        $('#Modal_btnUPDATE').css('display', 'none');
        $('#InsertModal').modal('show');
    })

    $('#btnEdit').on('click', function () {
        $('#Modal_CategoryID').attr('readonly', true);
        $('#Modal_btnINSERT').css('display', 'none');
        $('#Modal_btnUPDATE').css('display', '');
    })

    $('#Modal_btnINSERT').click(function () {
        Insert();
    })
})