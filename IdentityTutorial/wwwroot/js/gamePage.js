//CSS variables


//Global Variables
let playerCurrentPosition = Array.from(jsonData.currentCoordinateHistory.split(','));
let lastDirection = null;
let enemyCurrentDirectionHistory = Array.from(jsonData.enemyCurrentDirectionHistory.split(','));
let isEnemyMapFrozen = true;
let frozenMap = [];

//global objects
let model = new Model();
let gameboard = new Gameboard();


$(document).ready(function () {
    document.getElementById('scroll-target').scrollIntoView(true);
});

let drawSysBtnId = () => {
    let sysBtns = $('.systems-box');
    sysBtns.each(function (i, obj) {
        //if ($(this).html() == "") {
            $(this).html($(this).attr('id'));
        //}
    });
};

//other functions
let invertBool = (bool) => {

    if (bool) {
        return false;
    } else {
        return true;
    }
};

//Event Listners
$(() => {
    $(window).resize(function () {
    });

    /* conrols hover effect on game board */
    $('.game-square').on('mouseenter mouseleave', function (e) {
        gameboard.hoverOnBoard($(this), e);
    });
    $('.game-square').on('click', function () {
        gameboard.clickOnBoard($(this));
    });

    /* controls for panel group cards */
    $('#powers-btn').on('click', function () {
        if ($('#powers-display').hasClass('hide')) {
            $('#powers-display').removeClass('hide');
        } else {
            $('#powers-display').addClass('hide');
        }
    });

    $('#systems-btn').on('click', function () {
        if ($('#systems-display').hasClass('hide')) {
            $('#systems-display').removeClass('hide');
        } else {
            $('#systems-display').addClass('hide');
        }
    });

    $('#messages-btn').on('click', function () {
        if ($('#messages-display').hasClass('hide')) {
            $('#messages-display').removeClass('hide');
        } else {
            $('#messages-display').addClass('hide');
        }
    });

    $('#map-btn').on('click', function () {
        if ($('#map-controls-display').hasClass('hide')) {
            $('#map-controls-display').removeClass('hide');
        } else {
            $('#map-controls-display').addClass('hide');
        }
    });

    $(Helper.placeFirstPieceUiId).on('click', function () {
        gameboard.toggleMapControls($(this));
    });

    $(Helper.enemySlideActiveUiId).on('click', function () {
        gameboard.toggleMapControls($(this));
    });

    $(Helper.thirdThingUiId).on('click', function () {
        gameboard.toggleMapControls($(this));
    });
    $(Helper.showCellNumbers).on('click', function () {
        gameboard.toggleMapControls($(this));
    });
    $(Helper.clearCellNumbers).on('click', function () {
        gameboard.toggleMapControls($(this));
    });

    $(Helper.submitBtnId).on('click', function () {
        $(Helper.formId).submit();
    });

    /* controls the ui elements in panels */

    $('.circle').on('click', function () {
        model.setSystems($(this));
    });

    $('.panel-row').on('click', function () {
        model.setPower($(this));
    });

    $('.direction-label').on('click', function () {
        model.setDirection($(this));
    });

    // TODO: doesn't trigger??
    $('#model-messages-collapse-icon').on('click', function () {
    });
});