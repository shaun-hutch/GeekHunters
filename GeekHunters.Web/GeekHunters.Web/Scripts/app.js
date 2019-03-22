var firstLoad = true;
var selectedFilter = null;

$(function () {
    reloadCards();

    $("#frmUser").submit(function (e) {
        e.preventDefault();

        if ($("#selSkills").val().length === 0) {
            $(".select-validate").html("Please select a value.");
            return false;
        }


        $.ajax({
            url: "/Home/AddCandidate",
            type: "POST",
            data: {
                firstName: $("#first_name").val(),
                lastName: $("#last_name").val(),
                skillList: $("#selSkills").val().join(','),
            },
            success: function () {
                reloadCards();
            },
            error: function (data) {
                displayErrorMessage(data);
            }
        });
    });

    $("#selSkills").on("change", function () {
        if ($(this).val().length > 0) {
            $(".select-validate").html("");
        }
    });

    $("#skillList").on("click", ".collection-item", function () {
        $("#skillList").find("li").removeClass("selected");
        $(this).addClass("selected");
        selectedFilter = $(this).attr("skillid");
        filter(selectedFilter);
    });

    $("#btnClearFilter").on("click", function () {
        $("#skillList").find("li").removeClass("selected");
        selectedFilter = null;
        filter(selectedFilter);
    });
});

function reloadCards() {
    clearFields();
    resetFilter();
    loadCandidates();
    loadSkills();
}

function clearFields() {
    $("#first_name").val("");
    $("#last_name").val("");
    $("#selSkills").val("");
}

function filter(selectedFilter) {
    $("#geekList").find("li").each(function () {
        if (!selectedFilter) {
            $(this).show();
            return true;
        }
        var skillList = $(this).attr("skillList");
        var splitSkillList = skillList.split(",");

        if (jQuery.inArray(selectedFilter, splitSkillList) != -1) {
            $(this).show();
        }
        else {
            $(this).hide();
        }
    });
}

function resetFilter() {
    $("#skillList").find("li").removeClass("selected");
    $("#geekList").find("li").show();
    selectedFilter = null;
}


//this init method needs to be fired once the selects have been put on the page
function setupMaterializeSelect() {
    $('select').formSelect();
}

function loadCandidates() {
    $.ajax({
        url: "/Home/Candidates",
        type: "GET",
        success: function (data) {
            displayCandidates(data);
        },
        error: function (data) {
            displayErrorMessage(data);
        }
    });
}

function loadSkills() {
    $.ajax({
        url: "/Home/Skills",
        type: "GET",
        success: function (data) {
            displaySkills(data, setupMaterializeSelect);
        },
        error: function (data) {
            displayErrorMessage(data);
        }
    });
}

function displayCandidates(data) {
    var html = "";
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            html += "<li class=\"collection-item\" CandidateID=\"" + data[i].Id + "\" SkillList=\"" + data[i].Skills + "\">" + data[i].FirstName + " " + data[i].LastName + "</li>";
        }

        $("#geekList").html(html);
    }
}

function displaySkills(data, cb) {
    var listHtml = "";
    var selectHtml = "";
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            listHtml += "<li class=\"collection-item\" SkillID=\"" + data[i].Id + "\">" + data[i].Name + "</li>";
            selectHtml += "<option value=\"" + data[i].Id + "\">" + data[i].Name + "</option>";
        }

        $("#skillList").html(listHtml);
        $("#selSkills").html(selectHtml);
    }

    cb = cb || function () { };
    cb();
}

function displayErrorMessage(data) {
    console.log(data);
}

