$(function () {
    reloadCards();
});

function reloadCards() {
    loadCandidates();
}

function loadCandidates() {
    $.ajax({
        url: "/Home/Candidates",
        type: "GET",
        success: function (data) {
            displayCandidates(data);
            loadSkills();
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
            displaySkills(data);
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
            html += "<li class=\"collection-item\" CandidateID=" + data[i].Id + ">" + data[i].FirstName + " " + data[i].LastName + "</li>";
        }

        $("#geekList").html(html);
    }
}

function displaySkills(data) {
    var html = "";
    if (data.length > 0) {
        for (var i = 0; i < data.length; i++) {
            html += "<li class=\"collection-item\" SkillID=" + data[i].Id + ">" + data[i].Name + "</li>";
        }

        $("#skillList").html(html);
    }
}

function displayErrorMessage(data) {
    console.log(data);
}