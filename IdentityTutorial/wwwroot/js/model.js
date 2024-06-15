class Model {
    constructor() {
        //max value for powers for form validation
        this.healthMax = jsonData.maxHealth;
        this.minesMax = jsonData.maxMines;
        this.dronesMax = jsonData.maxDrones;
        this.sneakMax = jsonData.maxSneak;
        this.torpedoMax = jsonData.maxTorpedo;
        this.sonarMax = jsonData.maxSonar

        //value of powers
        this.healthValue = jsonData.healthValue;
        this.minesValue = jsonData.minesValue;
        this.dronesValue = jsonData.dronesValue;
        this.sneakValue = jsonData.sneakValue;
        this.torpedoValue = jsonData.torpedoValue;
        this.sonarValue = jsonData.sonarValue;

        //power strings
        this.health = jsonData.health;
        this.mines = jsonData.mines;
        this.drones = jsonData.drones;
        this.sneak = jsonData.sneak;
        this.torpedo = jsonData.torpedo;
        this.sonar = jsonData.sonar;

        //direction strings
        this.west = jsonData.west;
        this.north = jsonData.north;
        this.south = jsonData.south;
        this.east = jsonData.east;
        this.surface = jsonData.surface;

        //parse systems button data from server format
        this.systemsAttack = Helper.parseCsvToArrays(jsonData.systemsAttack);
        this.systemsDetect = Helper.parseCsvToArrays(jsonData.systemsDetect);
        this.systemsEvade = Helper.parseCsvToArrays(jsonData.systemsEvade);
        this.systemsReactor = Helper.parseCsvToArrays(jsonData.systemsReactor);

        this.systemsState = this.generateSystemsStateArray(
            this.systemsAttack,
            this.systemsDetect,
            this.systemsEvade,
            this.systemsReactor
        );

        Model.drawSystemsBtns(this.systemsState);

        //variables for the model
        this.selectedSystems = null; //id
        this.selectedDirection = null; //id
        this.selectedPower = null; //id
        this.firstMove = null;
        this.activatedPower = null;
        this.activatedPowerValue = null;
        this.message = null;
        this.selectedSonarTrueType = null;
        this.selectedSonarTrueValue = null;
        this.selectedSonarFalseType = null;
        this.selectedSonarFalseValue = null;

        this.prevSelectedSystems = null;
        this.prevSelectedDirection = null;
        this.prevSelectedPower = null
        this.prevFirstMove = null;
        this.prevActivatedPower = null;
        this.prevActivatedPowerValue = null;
        this.prevMessage = null;
        this.prevSelectedSonarTrueType = null;
        this.prevSelectedSonarTrueValue = null;
        this.prevSelectedSonarFalseType = null;
        this.prevSelectedSonarFalseValue = null;

        //used by model, but not returned to server. represents the direction that the systems button is pointing
        //this is used to prevent constantly having to look value up when new direction is set
        this.selectedSystemsDirection;

        //checks to see if all of the powers are charged, if they are, set this.selectedPower to 'allCharged'
        this.checkPowerChargedLevels();

        //flag for when it is the players first move.
        //Can be set to true by gameboard after it parses the player move history with model.setIsFirstTurn
        this.isFirstTurn = false;
    }

    // Information on how ot update elements in the UI
    //selectedUiElements = () => {
    //    let uiItems = [
    //        {
    //            name: 'selectedSystems',
    //            suffix: function () { return null },
    //            id: function () { return model.selectedSystems },
    //            prevId: function () { return model.prevSelectedSystems },
    //            uiStyle: function () { return Helper.systmesUiSelected },
    //            addCharacter: function () { return null },
    //            removeCharacter: function () { return null },
    //            remove: function () { throw new DOMException('not yet implemented') },
    //            add: function () { throw new DOMException('not yet implemented') }
    //        },
    //        {
    //            name: 'selectedDirection',
    //            suffix: function () { return null },
    //            id: function () { return model.selectedDirection },
    //            prevId: function () { return model.prevSelectedDirection },
    //            uiStyle: function () { return Helper.directionUiSelected },
    //            addCharacter: function () { return null },
    //            removeCharacter: function () { return null },
    //            remove: function () { throw new DOMException('not yet implemented') },
    //            add: function () { throw new DOMException('not yet implemented') }
    //        },
    //        {
    //            name: 'selectedPower',
    //            suffix: function () { return 'next-' },
    //            id: function () { return model.selectedPower },
    //            prevId: function () { return model.prevSelectedPower },
    //            uiStyle: function () { return Helper.powerUiSelected },
    //            addCharacter: function () { return Helper.nextPowerSelectedText },
    //            removeCharacter: function () { return Helper.nextPowerUnselectedText },
    //            remove: function () { throw new DOMException('not yet implemented') },
    //            add: function () { throw new DOMException('not yet implemented') }

    //        },
    //        {
    //            name: 'activatedPower',
    //            suffix: function () { return 'filled-' },
    //            id: function () { return model.activatedPower },
    //            prevId: function () { return model.prevActivatedPower },
    //            uiStyle: function () { return Helper.activatedPowerUiSelected },
    //            addCharacter: function () {
    //                let output;
    //                model.activatedPowerValue == null ?
    //                    output = Helper.chargedPowerSelectedText :
    //                    output = Helper.chargedPowerConfirmedText
    //                return output;
    //            },
    //            removeCharacter: function () { return Helper.chargedPowerUnselectedText },
    //            remove: function () { throw new DOMException('not yet implemented') },
    //            add: function () { throw new DOMException('not yet implemented') }
    //        },
    //        {
    //            name: 'activatedPowerValue',
    //            suffix: function () { return 'b' },
    //            id: function () { return model.activatedPowerValue },
    //            prevId: function () { return model.prevActivatedPowerValue },
    //            uiStyle: function () { return Helper.boxIconClass },
    //            addCharacter: function () {
    //                let rotateFlag = false;
    //                if (model.activatedPower == jsonData.torpedo) { rotateFlag = true }
    //                return Helper.infoIconBuilder(model.activatedPower, rotateFlag)
    //            },
    //            removeCharacter: function () { return model.prevActivatedPowerValue },
    //            remove: function () { throw new DOMException('not yet implemented') },
    //            add: function () { throw new DOMException('not yet implemented') }
    //        },
    //        // apv removed in favor of having everything split up into its own element object
    //        // apv effects for the different apvs
    //        {
    //            name: 'apvTarget',
    //            suffix: function () { return 'target-' },
    //            id: function () { return model.activatedPower },
    //            prevId: function () { return model.prevActivatedPower },
    //            uiStyle: function () { return 'd-none' },
    //            addCharacter: function () {
    //                if (model.activatedPowerValue === null) { return null }
    //                return Helper.getCellAddress($('#b' + model.activatedPowerValue))
    //            },
    //            removeCharacter: function () { return 'XX' },
    //            remove: function () { throw new DOMException('not yet implemented') },
    //            add: function () { throw new DOMException('not yet implemented') }
    //        },
    //        {
    //            name: 'firstMove',
    //            suffix: function () { return 'b' },
    //            id: function () { return this.firstMove },
    //            prevId: function () { return model.prevFirstMove },
    //            uiStyle: function () { return Helper.playerHeadUiSelected },
    //            addCharacter: function () { return null },
    //            removeCharacter: function () { return null },
    //            remove: function () { throw new DOMException('not yet implemented') },
    //            add: function () { throw new DOMException('not yet implemented') }
    //        }
    //    ];
    //    return uiItems;
    //}

    // Information on how ot update elements in the UI
    selectedUiElements = () => {
        let uiItems = [
            {
                name: 'selectedSystems',
                suffix: null,
                id: model.selectedSystems,
                prevId: model.prevSelectedSystems,
                uiStyle: Helper.systmesUiSelected,
                addCharacter: null,
                removeCharacter: null,
                remove: function () { $(`#${this.prevId}`).removeClass(this.uiStyle) },
                add: function () { $(`#${this.id}`).addClass(this.uiStyle) }
            },
            {
                name: 'selectedDirection',
                suffix: null,
                id: model.selectedDirection,
                prevId: model.prevSelectedDirection,
                uiStyle: Helper.directionUiSelected,
                addCharacter:null,
                removeCharacter: null,
                remove: function () { $(`#${this.prevId}`).removeClass(this.uiStyle) },
                add: function () { $(`#${this.id}`).addClass(this.uiStyle) }
            },
            {
                name: 'selectedPower',
                suffix: 'next-',
                id: model.selectedPower,
                prevId: model.prevSelectedPower,
                uiStyle: Helper.powerUiSelected,
                addCharacter: Helper.nextPowerSelectedText,
                removeCharacter: Helper.nextPowerUnselectedText,
                remove: function () {
                    $(`#${this.suffix}${this.prevId}`).removeClass(this.uiStyle);
                    $(`#${this.suffix}${this.prevId}`).html(this.removeCharacter);
                },
                add: function () {
                    $(`#${this.suffix}${this.id}`).addClass(this.uiStyle);
                    $(`#${this.suffix}${this.id}`).html(this.addCharacter);
                }

            },
            {
                name: 'activatedPower',
                suffix: 'filled-',
                id: model.activatedPower,
                prevId: model.prevActivatedPower,
                uiStyle: function () {
                    let output;
                    model.activatedPowerValue == null ?
                        output = Helper.activatedPowerUiSelected :
                        output = Helper.activatedPowerUiSet
                    return output;
                },
                addCharacter: function () {
                    let output;
                    model.activatedPowerValue == null ?
                        output = Helper.chargedPowerSelectedText :
                        output = Helper.chargedPowerConfirmedText
                    return output;
                },
                removeCharacter: Helper.chargedPowerUnselectedText,
                remove: function () {
                    console.log(`in remove(). uiStyle to remove: ${this.uiStyle()}`)
                    $(`#${this.suffix}${this.prevId}`).removeClass(this.uiStyle());
                    $(`#${this.suffix}${this.prevId}`).html(this.removeCharacter);
                },
                add: function () {
                    $(`#${this.suffix}${this.id}`).addClass(this.uiStyle());
                    $(`#${this.suffix}${this.id}`).html(this.addCharacter());
                }
            },

            //{
            //    name: 'activatedPowerValue',
            //    suffix: 'b',
            //    id: model.activatedPowerValue,
            //    prevId: model.prevActivatedPowerValue,
            //    uiStyle: Helper.boxIconClass,
            //    addCharacter: function () {
            //        let rotateFlag = false;
            //        if (model.activatedPower == jsonData.torpedo) { rotateFlag = true }
            //        return Helper.infoIconBuilder(model.activatedPower, rotateFlag)
            //    },
            //    removeCharacter: model.prevActivatedPowerValue,
            //    remove: function () { throw new DOMException('not yet implemented') },
            //    add: function () { throw new DOMException('not yet implemented') }
            //},

            // apv removed in favor of having everything split up into its own element object
            // apv effects for the different apvs

            {   // TODO: support reporting of Sonar activation (handled in the sonar drawing section)
                name: 'apvTarget',
                suffix: 'target-',
                id: model.activatedPower,
                prevId: model.prevActivatedPower,
                uiStyle: 'd-none',
                addCharacter: function () {
                    if (model.activatedPowerValue === null) { return null }
                    return Helper.getCellAddress($('#b' + model.activatedPowerValue))
                },
                removeCharacter: 'XX',
                remove: function () { throw new DOMException('not yet implemented') },
                add: function () { throw new DOMException('not yet implemented') }
            },
            { //TODO: Dont! This one stays like this
                name: 'firstMove',
                suffix: 'b',
                id: this.firstMove,
                prevId: model.prevFirstMove,
                uiStyle: Helper.playerHeadUiSelected,
                addCharacter: null,
                removeCharacter: null,
                remove: function () { throw new DOMException('not yet implemented') },
                add: function () { throw new DOMException('not yet implemented') }
            },
            {   // TODO: this selected power on the board
                name: jsonData.mines,
                suffix: 'b',
                id: model.activatedPowerValue,
                prevId: model.prevActivatedPowerValue,
                remove: function () {
                    $(`#${this.suffix}${this.prevId}`).html(this.prevId)
                },
                add: function () {
                    const div = $(`#${this.suffix}${this.id}`);
                    const overlayString = Helper.boardBoxOverlayBuilder(Helper.boxOverlaySuffix + this.id);
                    const imageString = Helper.boardIconBuilder(this.name);
                    div.html('');
                    div.append(overlayString); $(`#${Helper.boxOverlaySuffix}${this.id}`).append(imageString);
                }
            },
            {   // TODO: this selected power on the board
                name: jsonData.drones,
                suffix: 'o',
                id: model.activatedPowerValue,
                prevId: model.prevActivatedPowerValue,
                remove: function () { $(`#${this.suffix}${this.prevId}`).addClass('d-none') },
                add: function () { $(`#${this.suffix}${this.id}`).removeClass('d-none') }
            },
            {   // TODO: this selected power on the board
                name: jsonData.sneak,
                suffix: 'b',
                id: model.activatedPowerValue,
                prevId: model.prevActivatedPowerValue,
                remove: function () { $(`#${this.suffix}${this.prevId}`).html(this.prevId) },
                add: function () {
                    const div = $(`#${this.suffix}${this.id}`);
                    const overlayString = Helper.boardBoxOverlayBuilder(Helper.boxOverlaySuffix + this.id);
                    const imageString = Helper.boardIconBuilder(this.name, true);
                    div.html('');
                    div.append(overlayString); $(`#${Helper.boxOverlaySuffix}${this.id}`).append(imageString);
                }
            },
            {   // TODO: this selected power on the board
                name: jsonData.torpedo,
                suffix: 'b',
                id: model.activatedPowerValue,
                prevId: model.prevActivatedPowerValue,
                remove: function () { $(`#${this.suffix}${this.prevId}`).html(this.prevId) },
                add: function () {
                    const div = $(`#${this.suffix}${this.id}`);
                    const overlayString = Helper.boardBoxOverlayBuilder(Helper.boxOverlaySuffix + this.id);
                    const imageString = Helper.boardIconBuilder(this.name, true);
                    div.html('');
                    div.append(overlayString); $(`#${Helper.boxOverlaySuffix}${this.id}`).append(imageString);
                }
            },
            {   // TODO: this selected power shown to player in the location box that otherwise displays coords
                name: jsonData.sonar,
                suffix: 'target-',
                id: model.activatedPowerValue,
                prevId: model.prevActivatedPowerValue,
                uiStyle: Helper.boxIconClass,
                addCharacter: null,
                removeCharacter: null,
                remove: function () { throw new DOMException('not yet implemented') },
                add: function () { throw new DOMException('not yet implemented') }
            },
            {   // TODO: this detonating mine (not being placed, when its selected to go off)
                name: 'mineDetonation',
                suffix: 'b',
                //id: model.activatedPowerValue,
                //prevId: model.prevActivatedPowerValue,
                //uiStyle: Helper.boxIconClass,
                addCharacter: null,
                removeCharacter: null,
                remove: function () { throw new DOMException('not yet implemented') },
                add: function () { throw new DOMException('not yet implemented') }
            },
            {   // TODO: this power effect for torpedo and mine explosion
                name: 'blastRadius',
                suffix: 'b',
                id: model.activatedPowerValue,
                prevId: model.prevActivatedPowerValue,
                uiStyle: Helper.boxIconClass,
                addCharacter: null,
                removeCharacter: null,
                remove: function () { throw new DOMException('not yet implemented') },
                add: function () { throw new DOMException('not yet implemented') }
            },
            {   // TODO: this power effect for a line following the sneaking sub
                name: 'sneakPath',
                suffix: 'b',
                id: model.activatedPowerValue,
                prevId: model.prevActivatedPowerValue,
                uiStyle: Helper.boxIconClass,
                addCharacter: null,
                removeCharacter: null,
                remove: function () { throw new DOMException('not yet implemented') },
                add: function () { throw new DOMException('not yet implemented') }
            },
            {   // TODO: this is the new head of the sub
                name: 'newSubHead',
                suffix: 'b',
                //id: this.firstMove,
                //prevId: model.prevFirstMove,
                //uiStyle: Helper.playerHeadUiSelected,
                addCharacter: null,
                removeCharacter: null,
                remove: function () { throw new DOMException('not yet implemented') },
                add: function () { throw new DOMException('not yet implemented') }
            }
        ];
        return uiItems;
    }

    //TODO: this shouldn't be static || maybe it should be. idk
    generateSystemsStateArray = (a, d, e, r) => {
        let systemsStateArray = [
            {
                system: 'Attack',
                index: 0,
                value: a[0],
                htmlId: 'w0',
                pipe: 0,
                direction: this.west
            },
            {
                system: 'Evade',
                index: 0,
                value: e[0],
                htmlId: 'w1',
                pipe: 0,
                direction: this.west
            },
            {
                system: 'Detect',
                index: 0,
                value: d[0],
                htmlId: 'w2',
                pipe: 0,
                direction: this.west
            },
            {
                system: 'Detect',
                index: 1,
                value: d[1],
                htmlId: 'w3',
                pipe: null,
                direction: this.west
            },
            {
                system: 'Reactor',
                index: 0,
                value: r[0],
                htmlId: 'w4',
                pipe: null,
                direction: this.west
            },
            {
                system: 'Reactor',
                index: 1,
                value: r[1],
                htmlId: 'w5',
                pipe: null,
                direction: this.west
            },
            {
                system: 'Evade',
                index: 1,
                value: e[1],
                htmlId: 'n0',
                pipe: 1,
                direction: this.north
            },
            {
                system: 'Attack',
                index: 1,
                value: a[1],
                htmlId: 'n1',
                pipe: 1,
                direction: this.north
            },
            {
                system: 'Evade',
                index: 2,
                value: e[2],
                htmlId: 'n2',
                pipe: 1,
                direction: this.north
            },
            {
                system: 'Detect',
                index: 2,
                value: d[2],
                htmlId: 'n3',
                pipe: null,
                direction: this.north
            },
            {
                system: 'Attack',
                index: 2,
                value: a[2],
                htmlId: 'n4',
                pipe: null,
                direction: this.north
            },
            {
                system: 'Reactor',
                index: 2,
                value: r[2],
                htmlId: 'n5',
                pipe: null,
                direction: this.north
            },
            {
                system: 'Detect',
                index: 3,
                value: d[3],
                htmlId: 's0',
                pipe: 2,
                direction: this.south
            },
            {
                system: 'Evade',
                index: 3,
                value: e[3],
                htmlId: 's1',
                pipe: 2,
                direction: this.south
            },
            {
                system: 'Attack',
                index: 3,
                value: a[3],
                htmlId: 's2',
                pipe: 2,
                direction: this.south
            },
            {
                system: 'Attack',
                index: 4,
                value: a[4],
                htmlId: 's3',
                pipe: null,
                direction: this.south
            },
            {
                system: 'Reactor',
                index: 3,
                value: r[3],
                htmlId: 's4',
                pipe: null,
                direction: this.south
            },
            {
                system: 'Evade',
                index: 4,
                value: e[4],
                htmlId: 's5',
                pipe: null,
                direction: this.south
            },
            {
                system: 'Detect',
                index: 4,
                value: d[4],
                htmlId: 'e0',
                pipe: 1,
                direction: this.east
            },
            {
                system: 'Evade',
                index: 5,
                value: e[5],
                htmlId: 'e1',
                pipe: 2,
                direction: this.east
            },
            {
                system: 'Attack',
                index: 5,
                value: a[5],
                htmlId: 'e2',
                pipe: 0,
                direction: this.east
            },
            {
                system: 'Reactor',
                index: 4,
                value: r[4],
                htmlId: 'e3',
                pipe: null,
                direction: this.east
            },
            {
                system: 'Detect',
                index: 5,
                value: d[5],
                htmlId: 'e4',
                pipe: null,
                direction: this.east
            },
            {
                system: 'Reactor',
                index: 5,
                value: r[5],
                htmlId: 'e5',
                pipe: null,
                direction: this.east
            }
        ];
        return systemsStateArray
    };

    //check to see if all of the power values === max, if they do
    //set powers to 'allCharged'
    checkPowerChargedLevels() {

        if (this.minesMax === this.minesValue &&
            this.dronesMax === this.dronesValue &&
            this.sneakMax === this.sneakValue &&
            this.torpedoMax === this.torpedoValue &&
            this.sonarMax === this.sonarValue) {

            this.selectedPower = jsonData.allCharged;
        }
        
    }

    //TODO: this should go into the helper class, or the gameboard class maybe?
    static drawSystemsBtns = (btnsData) => {

        let trueClass = 'green-bg-br';
        let falseClass = 'red-bg-br';

        btnsData.forEach((item) => {
            let div = $('#' + item.htmlId);
            item.value ?
                Helper.addRemoveClass(div, trueClass) :
                Helper.addRemoveClass(div, falseClass);
        });
    };

    setIsFirstTurn() {

        this.isFirstTurn = true;

        this.selectedSystems = jsonData.firstMove;
        this.selectedSystemsDirection = jsonData.firstMove;
        this.selectedDirection = jsonData.firstMove;
    }

    getSystemsState() {
        return this.systemsState;
    }

    checkModel() {
        let isGood = true;
        let result = [];

        //disable submit btn until all checks have passed
        let submit = $('#submit-btn');
        let uiElements = this.selectedUiElements();
        Helper.addRemoveClass(submit, 'disabled');

        //clear styling from UI elements
        Helper.clearUiSelected(uiElements);

        //verify that all required fields are populted

        //check first move
        if (this.isFirstTurn) {
            if (this.firstMove == null) {
                isGood = false;
                result[result.length] = "starting cell must be selected";
            }
        }

        //increment powers
        //TODO: add support for if they are all charged
        if (this.selectedPower === null) {
            isGood = false;
            result[result.length] = "charging power is required";
        }

        //systems degredation
        if (this.selectedSystems === null) {
            isGood = false;
            result[result.length] = "systems must be selected";
        }

        //direction
        if (this.selectedDirection === null) {
            isGood = false;
            result[result.length] = "direction must be entered";
        }

        //systems direction and direction must match
        if (this.selectedSystemsDirection != this.selectedDirection) {
            isGood = false;
            result[result.length] = "selected systems direction and direction do not match"
        }

        //when activating a power, the activating power value must also be set
        if (this.activatedPower != null) {
            if (this.activatedPowerValue == null) {
                isGood = false;
                result[result.length] = "activated power value must be set when a power is being activated"
            }
        }

        //if there were no errors
        if (isGood) {
            this.setFormForPost();
            Helper.addRemoveClass(submit, null, 'disabled');
            result[result.length] = 'Ready to Submit Turn!';
        }

        //update styling and return the result of the  model check
        Helper.addUiSelected(uiElements);

        // Update the model messages list with the results of the check
        this.clearModelMessages();
        this.postModelMessages(result);

        return result;
    }

    // Adds strings from array into the model messages list
    postModelMessages(messagesArray) {

        // TODO: fix this magic string
        if (messagesArray[0] === 'Ready to Submit Turn!') {
            $(Helper.modelMessagesHeaderId).append(`<div class="text-success fw-bolder">${messagesArray[0]}</div>`);
        }

        // TODO: end this string suffering
        else {
            $(Helper.modelMessagesHeaderId).append(`<div class="text-danger fw-bolder">Captain, do the following to finish your turn: </div >
    <a class="info-icon" data-bs-toggle="collapse" href="${Helper.modelMessagesTargetId}" role="button">
<span><img class="info-icon south-arrow" src="/images/collapse-arrow-icon.svg" id="model-messages-collapse-icon" /></span></a>`);
            messagesArray.forEach(x => {
                $(Helper.modelMessagesListId).append(
                    `<li class="list-group-item text-dark bg-transparent">${x}</li>`
                );
            });
        }

        
    }

    // Clears the messages in the model messages list
    clearModelMessages() {
        $(Helper.modelMessagesHeaderId).html('');
        $(Helper.modelMessagesListId).html('');
    }

    setFormForPost() {

        //form inputs
        let selectedDirectionInput = $('#direction-input');
        let selectedSystemsInput = $('#systems-input');
        let selectedPowerInput = $('#power-input');
        let firstMoveInput = $('#first-move-input');
        let activatedPowerInput = $('#activated-power-input');
        let activatedPowerValueInput = $('#activated-power-value-input');
        let messageInput = $('#message-input');
        let selectedSonarTrueTypeInput = $('#sonar-true-type-input');
        let selectedSonarTrueValueInput = $('#sonar-true-value-input');
        let selectedSonarFalseTypeInput = $('#sonar-false-type-input');
        let selectedSonarFalseValueInput = $('#sonar-false-value-input');

        //get information from model to populate these fields?
        selectedDirectionInput.val(this.selectedDirection);
        selectedSystemsInput.val(this.selectedSystems);
        selectedPowerInput.val(this.selectedPower);
        activatedPowerInput.val(this.activatedPower);
        activatedPowerValueInput.val(this.activatedPowerValue);
        messageInput.val(this.messageInput);
        selectedSonarTrueTypeInput.val(this.selectedSonarTrueType);
        selectedSonarTrueValueInput.val(this.selectedSonarTrueValue);
        selectedSonarFalseTypeInput.val(this.selectedSonarFalseType);
        selectedSonarFalseValueInput.val(this.selectedSonarFalseValue);
        if (this.firstMove != null) { firstMoveInput.val(this.firstMove); }
    }

    //Verify that the systems and directions buttons match when entered by user
    setSystems(div) {

        //if it is the first turn then do nothing
        if (this.isFirstTurn) { return false; }

        let id = div.attr('id');
        let systemsObj = this.systemsState.find(({ htmlId }) => htmlId == id);

        // used to check for validity of movement
        let tempSystemsDirection = systemsObj.direction;
        let isAvailable = systemsObj.value;

        //if the selected button is false, then it cannot be selected
        //the proccess is aborted
        if (!isAvailable) { return false }

        this.prevSelectedSystems = this.selectedSystems
        this.selectedSystems = id;
        this.selectedSystemsDirection = tempSystemsDirection;

        //call setDirection to change the directon to relative systems direction
        this.setDirection($('#' + this.selectedSystemsDirection));
    }

    //last point in direction involved interactions
    setDirection(div) {

        //if it is the first turn then do nothing
        if (this.isFirstTurn) { return false; }

        let id = div.attr('id');

        this.prevSelectedDirection = this.selectedDirection;
        this.selectedDirection = id;

        if (this.selectedSystems != null) {
            if (this.selectedSystemsDirection != this.selectedDirection) {
                this.prevSelectedSystems = this.selectedSystems;
                this.selectedSystems = null;
                this.selectedSystemsDirection = null;
            }
        }

        //if surfacing set the systems and systemsDir to be surface
        if (id === this.surface) {
            this.selectedSystems = this.surface;
            this.selectedSystemsDirection = this.surface;
        }

        //update the model since all dirction related changes have been made and update view
        this.checkModel();
    }

    // Sets both the selected power and the activated power based on model state
    /*
            
                // TODO:
            * The set power should break out into charge power and use power.
            * After the user selects a power to use they can click in the gameboard. After the board cells have been selected, the ACTIVATING state of the button 
                need to be changed to SET, and the toggle has to work


     */
    setPower(div) {

        let id = div.attr('id');
        let current = this[id + "Value"];
        let max = this[id + "Max"];
        let difference = max - current;

        //check if the power can be activated, or if it is being charged
        if (difference === 0) {

            // Reset all activated power value cells in the gameboard array and in the model
            this.prevActivatedPowerValue = this.activatedPowerValue;
            this.activatedPowerValue = null;
            gameboard.clearAllActivatedPowerValues();

            //allow for toggling of activating power
            if (this.activatedPower != id) {

                let powerObject;

                // Toggle off the previous activated power, if it exists
                if (this.activatedPower != null) {
                    powerObject = gameboard.powerControlsArray.find(x => x.name === this.activatedPower);
                    try {
                        //if the power value has been selected, then the power object toggle doesnt need to occur
                        if (this.prevActivatedPowerValue == null) {
                            powerObject.onToggle();
                        }

                    }
                    catch (e) {
                        console.error(e);
                    }
                }

                // set the activated power to the id of the clicked div
                this.prevActivatedPower = this.activatedPower;
                this.activatedPower = id;

                powerObject = gameboard.powerControlsArray.find(x => x.name === id);

                // try to toggle the powerObject display of the clicked div
                try {
                    powerObject.onToggle();
                }
                catch (e) {
                    console.error(e);
                }

            } else {

                // This happens when the player is activating a power for the first time per load

                let powerObject = gameboard.powerControlsArray.find(x => x.name === id);

                // try to toggle the powerObject display
                try {

                    if (this.prevActivatedPowerValue == null) {
                        powerObject.onToggle();
                    }
                }
                catch (e) {
                    console.error(e);
                }

                this.prevActivatedPower = this.activatedPower;
                this.activatedPower = null;

            }

        } else {
            this.prevSelectedPower = this.selectedPower;
            this.selectedPower = id;
        }

        //update view and check model
        this.checkModel();
    }

    // TODO: need to set all activated powers as null at some point when a new set power is set and when setactivatedpowervalue is set

    setActivatedPowerValue(div) {

        let cellNumber = $(div).attr('id').substring(1);
        this.prevActivatedPowerValue = this.activatedPowerValue
        this.activatedPowerValue = cellNumber;

        // this should be done in the view adjuster section
        // add thing in powerControlsArray and the string helper that say what the power-target id is....and add it to the play.cshtml file too

        // Maybe try to incorporate the gameboard into the model update checker thingy.

        // update the model
        this.checkModel();
    }
 
    setFirstPiece(div) {

        let divId = parseInt(div.attr('id').substring(1),10);

        this.prevFirstMove = this.firstMove;
        this.firstMove = divId;
        this.checkModel();
    }
}