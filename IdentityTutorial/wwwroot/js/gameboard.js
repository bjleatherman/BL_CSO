class Gameboard {
    constructor() {
        //map data
        this.mapLength = jsonData.mapLength;
        this.mapWidth = jsonData.mapWidth;
        this.islands = Helper.parseCsvToArrays(jsonData.islandCoordinates);
        this.topEdge = [];
        this.bottomEdge = [];
        this.rightEdge = [];
        this.leftEdge = [];

        //movement data
        this.totalCoordinateHistory = Helper.parseCsvToArrays(jsonData.totalCoordinateHistory, false);
        this.currentCoordinateHistory = Helper.parseCsvToArrays(jsonData.currentCoordinateHistory, false);
        this.enemyCurrentDirectionHistory = Helper.parseCsvToArrays(jsonData.enemyCurrentDirectionHistory, false);
        this.playerHead = this.totalCoordinateHistory[this.totalCoordinateHistory.length - 1];

        //will be used to let user freeze map when sliding enemy map
        this.isEnemyMapFrozen = true;

        //map controls bools
        //if statement detects when it is the players first move
        if (this.totalCoordinateHistory == "") {

            this.isMakingFirstMove = true;
            model.setIsFirstTurn();
        }
        else {
            this.totalCoordinateHistory = this.totalCoordinateHistory.map(Number);
        }

        // TODO: this may not be necessary
        if (this.currentCoordinateHistory == '') {
        }
        else {
            this.currentCoordinateHistory = this.currentCoordinateHistory.map(Number);

        }

        this.isEnemySlideActive = false;
        //this.isThirdThing = false;
        this.showCellNumbers = false;
        this.clearCellNumbers = true;

        // data for the Map controls panel buttons

        /*
         * Page Objects
         */

        this.mapControlsArray = [
            {
                name: 'placeFirstPiece',
                id: Helper.placeFirstPieceUiId,
                isActive: this.isMakingFirstMove,
                uiStyle: Helper.mapControlsUiSelected,
                hoverFunc: function (div, event) {
                    Gameboard.drawFirstMove(div, event);
                },
                clickFunc: function (div) {
                    Gameboard.clickFirstMove(div);
                }
            },
            {
                name: 'enemySlideActive',
                id: Helper.enemySlideActiveUiId,
                isActive: this.isEnemySlideActive,
                uiStyle: Helper.mapControlsUiSelected,
                hoverFunc: function () {
                    console.error(`Enemy Slide Not Yet Implemented`);
                },
                clickFunc: function (div) {
                    console.error(`Enemy Slide Not Yet Implemented`);
                }
            },
            {
                name: 'thirdThing',
                id: Helper.thirdThingUiId,
                isActive: this.isThirdThing,
                uiStyle: Helper.mapControlsUiSelected,
                hoverFunc: function () {
                    console.error(`Third Thing Yet Implemented`);
                },
                clickFunc: function (div) {
                    console.error(`Third Thing Yet Implemented`);
                }
            },
            {
                name: 'showCellNumbers',
                id: Helper.showCellNumbers,
                isActive: this.testPowerDraw,
                uiStyle: Helper.mapControlsUiSelected,
                hoverFunc: function () {
                    //console.error(`Third Thing Yet Implemented`);
                },
                clickFunc: function (div) {
                    //console.error(`Third Thing Yet Implemented`);
                },
                onToggle: function (div) {
                    $('.game-square').each(function () {
                        $(this).addClass('color-white');
                        $(this).removeClass('color-transparent');
                    });
                }
            },
            {
                name: 'clearCellNumbers',
                id: Helper.clearCellNumbers,
                isActive: this.clearTestPowerDraw,
                uiStyle: Helper.mapControlsUiSelected,
                hoverFunc: function () {
                    //console.error(`Third Thing Yet Implemented`);
                },
                clickFunc: function (div) {
                    //console.error(`Third Thing Yet Implemented`);
                },
                onToggle: function (div) {
                    $('.game-square').each(function () {
                        $(this).removeClass('color-white');
                        $(this).addClass('color-transparent');
                    });
                }
            },      
        ];
                
        //data for the power elements and their control of the gameboard
        this.powerControlsArray = [
            {
                name: jsonData.mines,
                panel: jsonData.mines,
                nextBtn: 'next-' + jsonData.mines,
                filledBtn: 'filled-' + jsonData.mines,
                gameboardUi: Helper.minesRangeClass,
                isActive: false,
                isCoordRequired: true,
                drawFunc: 'drawMinesRange',
                placedCell: null,
                placedAddress: null,
                hoverFunc: function (div, event) {
                    Gameboard.hoverMinesRange(div, event);
                },
                clickFunc: function (div) {
                    Gameboard.clickMinesRange(div);
                },
                onToggle: function () {
                    this.isActive ? this.isActive = false : this.isActive = true;
                    gameboard.toggleDrawPowerRange(this);
                }
            },
            {
                name: jsonData.drones,
                panel: jsonData.drones,
                nextBtn: 'next-' + jsonData.drones,
                filledBtn: 'filled-' + jsonData.drones,
                gameboardUi: Helper.dronesOverlayClass,
                isActive: false,
                isCoordRequired: true,
                drawFunc: 'drawDronesRange',
                placedCell: null,
                placedAddress: null,
                hoverFunc: function (div, event) {
                    Gameboard.hoverMinesRange(div, event);
                },
                clickFunc: function (div) {
                    Gameboard.clickDroneRange(div);
                },
                onToggle: function () {
                    this.isActive ? this.isActive = false : this.isActive = true;
                    gameboard.toggleDrawPowerRange(this);
                }
            },
            {
                name: jsonData.sneak,
                panel: jsonData.sneak,
                nextBtn: 'next-' + jsonData.sneak,
                filledBtn: 'filled-' + jsonData.sneak,
                gameboardUi: Helper.sneakRangeClass,
                isActive: false,
                drawFunc: 'drawSneakRange',
                isCoordRequired: true,
                placedCell: null,
                placedAddress: null,
                hoverFunc: function (div, event) {
                    Gameboard.hoverSneakRange(div, event);
                },
                clickFunc: function (div) {
                    Gameboard.clickSneakRange(div);
                },
                onToggle: function () {
                    this.isActive ? this.isActive = false : this.isActive = true;
                    gameboard.toggleDrawPowerRange(this);
                }
            },
            {
                name: jsonData.torpedo,
                panel: jsonData.torpedo,
                nextBtn: 'next-' + jsonData.torpedo,
                filledBtn: 'filled-' + jsonData.torpedo,
                gameboardUi: Helper.torpedoRangeClass,
                isActive: false,
                isCoordRequired: true,
                drawFunc: 'drawTorpedoRange',
                placedCell: null,
                placedAddress: null,
                hoverFunc: function (div, event) {
                    Gameboard.hoverTorpedoRange(div, event);
                },
                clickFunc: function (div) {
                    Gameboard.clickTorpedoRange(div)
                },
                onToggle: function () {
                    this.isActive ? this.isActive = false : this.isActive = true;
                    gameboard.toggleDrawPowerRange(this);
                }
            },
            {
                name: jsonData.sonar,
                panel: jsonData.sonar,
                nextBtn: 'next-' + jsonData.sonar,
                filledBtn: 'filled-' + jsonData.sonar,
                gameboardUi: Helper.gameboardPowerOverlayClass,
                isActive: false,
                isCoordRequired: false,
                placedCell: null,
                placedAddress: null,
                hoverFunc: function (div, event) {

                },
                clickFunc: function (div) {

                },
                onToggle: function () {

                }
            },
        ];

        if (this.currentCoordinateHistory[0] === '') {

            this.isFirstTurn = true;
        }

        Gameboard.drawGameboard(this.mapLength, this.mapWidth);
        Gameboard.drawIslands(this.islands);
        Gameboard.drawPlayer(this.currentCoordinateHistory);
        this.gridArray = this.getGridArray();
        this.insertDronesOverlay();

        this.findEdges(this.mapLength, this.mapWidth);

        
    }

    

    //TODO, these shouldn't be static || idk anymore maybe they can be
    static drawGameboard = (l, w) => {

        // TODO: update the site grid grid-templete columns repeat to accomodate l + 1 repeat 

        let totalBoxes = l * w;
        let topLeftCorner = 0;
        let topRightCorner = l - 1;
        let bottomLeftCorner = totalBoxes - l;
        let bottomRightCorner = totalBoxes - 1;
        let boardContainer = $(`#board-container`);

        for (let i = 0; i < totalBoxes; i++) {
            if (i % w == 0 && i != 0) {
                boardContainer.append(
                    `<div class="box legend column" id="r${i}">${i/w}</div>`);
            }
            boardContainer.append(
                `<div class="box water game-square color-transparent" id="b${i}">${i}</div>`);
                //`<div class="box water game-square" id="b${i}"></div>`);
        }

        boardContainer.append(`<div class="box legend column" id="r${totalBoxes}">${w}</div>`)

        for (let i = 0; i < l; i++) {
            if (i === 0) {
                boardContainer.prepend(
                    `<div class="box filler-box">`);
            }
            boardContainer.prepend(
                `<div class="box legend column" id="c${i}">${String.fromCharCode(97+l-1-i)}</div>`);
        }

        for (let i = 0; i < w; i++) {

        }

        $(`#c${l-1}`).addClass('br-tl');
        $(`.filler-box`).addClass('br-tr');
        $(`#r${totalBoxes}`).addClass('br-br');
        $(`#b${bottomLeftCorner}`).addClass('br-bl');
    };

    static drawIslands = (c) => {

        if (c[0] == '' || c=='') {
            return;
        }

        let islandClass = 'island';
        let waterClass = 'water';
        c.forEach((x) => {
            Helper.addRemoveClass($('#b' + x), islandClass, waterClass);
        });
    };

    static drawPlayer = (c) => {

        if (c[0] == '' || c == '') {
            return;
        }

        let path = c
        let playerClass = 'player';
        let playerHeadClass = 'player-head';

        Helper.addRemoveClass($('#b' + path[path.length - 1]), playerHeadClass);
        path.forEach(x => Helper.addRemoveClass($('#b' + x), playerClass));
    };
    
    findEdges() {
        let l = this.mapLength;
        let w = this.mapWidth
        let totalBoxes = l * w;

        for (let i = 0; i < l; i++) {
            //this.topEdge.push($(`#b${i}`));
            //this.bottomEdge.push($(`#b${totalBoxes - i - 1}`));
            //this.leftEdge.push($(`#b${i * l}`));
            //this.rightEdge.push($(`#b${ i * l + l - 1}`));

            this.topEdge.push(i);
            this.bottomEdge.push(totalBoxes - i - 1);
            this.leftEdge.push(i * l);
            this.rightEdge.push(i * l + l - 1);
        }

        //console.log(this.topEdge);
        //console.log(this.bottomEdge);
        //console.log(this.rightEdge);
        //console.log(this.leftEdge);
    }

    getGridArray() {

        const gridSize = jsonData.mapLength;
        const sectionSize = gridSize / 3;
        const grid = [];

        for (let i = 0; i < gridSize * gridSize; i++) {
            grid.push(i);
        }

        const sections = [];
        for (let row = 0; row < gridSize; row += sectionSize) {
            for (let col = 0; col < gridSize; col += sectionSize) {
                let section = [];
                for (let i = 0; i < sectionSize; i++) {
                    for (let j = 0; j < sectionSize; j++) {
                        let index = (row + i) * gridSize + (col + j);
                        section.push(grid[index]);
                    }
                }
                sections.push(section);
            }
        }
        return sections;
    }

    insertDronesOverlay() {
        let insertTargets = [0, 5, 10, 75, 80, 85, 150, 155, 160]
        let regions = [1, 2, 3, 4, 5, 6, 7, 8, 9];
        let regionsIndex = 0;

        insertTargets.forEach(x => {
            $('#b' + x).append(`<div class="${Helper.dronesOverlayClass} d-none" id="${'o' + (regions[regionsIndex] - 1)}"><div class="${Helper.zoneNumberClass }" id="z${regions[regionsIndex]}">${regions[regionsIndex]}</div></div>`);
            regionsIndex++;
        });
    }

    checkNewDirection(div) {
        //let divNum = div.attr('id');
        let divNum = 21
        let lastLocation = this.currentCoordinateHistory[this.currentCoordinateHistory.length - 1];

        /*checks to perform
         *
         * crossing an edge of the board
         *      top, bottom, left, or right
         *      
         * entering a space currently occupied by an island
         * 
         * entering a space previously occupied
         */

        if (this.currentCoordinateHistory.includes(divNum)) {
            return false;
        }

        if (this.currentCoordinateHistory.includes(divNum)) {
            return false;
        }

        if (lastLocation === '') {
            return true;
        }


    }

    toggleMapControls(div) {


        let divId = div.attr('id');

        this.mapControlsArray.forEach(x => {
            Helper.addRemoveClass($(x.id), null, x.uiStyle);
            Helper.addRemoveClass($(x.id), null, 'active');
            x.isActive = false;
        });

        Helper.toggleClass(div, Helper.mapControlsUiSelected);
        Helper.toggleClass(div, 'active');
        try {
            this.mapControlsArray.find(x => x.id === '#' + divId).isActive = true;
            let onToggleEvent = this.mapControlsArray.find(x => x.id === '#' + divId).onToggle();
            if (onToggleEvent != undefined) { onToggleEvent(div); }
        } catch (e) {
            console.info(`${this.mapControlsArray.find(x => x.id === '#' + divId).name} does not have an 'onToggleEvent'`);
            console.error(e);
        }
        

        //will likely have to clear things like the enemy map if it 
        //was displayed while there is a toggle that was pressed
    }

    hoverOnBoard(div, event) {
        let control = this.mapControlsArray.find(x => x.isActive === true);
        let power = this.powerControlsArray.find(x => x.isActive === true);

        if (control != undefined) {
            control.hoverFunc(div, event);
        }
        //Where the mouse enter and mouse leave functions are
        if (event.type === 'mouseenter') {

        }
        if (event.type === 'mouseleave') {

        }

        if (power != undefined && div.hasClass(power.gameboardUi)) {
            power.hoverFunc(div, event);
        }
    }

    clickOnBoard(div) {
        let control = this.mapControlsArray.find(x => x.isActive === true);
        let power = this.powerControlsArray.find(x => x.isActive === true);

        if (control != undefined) {
            control.clickFunc(div, event);
        }

        if (power != undefined &&
            (div.hasClass(power.gameboardUi) ||
            $(div).find('.' + power.gameboardUi).length)) {
            power.clickFunc(div);
        }
    }

    static drawFirstMove(div, event) {

        //Where the mouse enter and mouse leave functions are
        if (event.type === 'mouseenter') {

            //check if piece is in an island
            if (!gameboard.checkIfDivIsIsland(div)) {
                Helper.addRemoveClass(div, Helper.playerHeadUiSelectedHover);
            }
        }
        if (event.type === 'mouseleave') {
            Helper.addRemoveClass(div, null, Helper.playerHeadUiSelectedHover);

        }
    }

    static clickFirstMove(div) {
        //Veryify that piece is not occupied by an island
        if (!gameboard.checkIfDivIsIsland(div)) {
            model.setFirstPiece(div);
        }
    }

    checkIfDivIsIsland(div) {

        if (this.islands == '') { return false; }

        // TODO: There are undefineds being passed in from helper.getValidAdjacentCells when the code is checking
        //      for values to the south of a cell (maybe other directions too, though that hasnt been observed yet).
        //      Setting an if statement to detect if div is undefined has not been triggering(????), 
        //      if the code fails(due to div being undefined), the catch will return that the cell is not valid b/c out of bounds

        try {
            let divId = div.attr('id');
            let cellNumber = parseInt(divId.substring(1), 10);

            return this.islands.includes(cellNumber);
        } catch {
            return false;
        }
        
    }

    isDivInCurrentCoordHist(div) {

        if (this.currentCoordinateHistory == '') { return false; }
        try {
            let divId = div.attr('id');
            let cellNumber = parseInt(divId.substring(1), 10);

            return this.currentCoordinateHistory.includes(cellNumber);
        } catch {
            return false;
        }
    }

    /*
     * Power Toggles
     */

    toggleDrawPowerRange(context) {

        if (context.isActive) {
            gameboard[context.drawFunc]();
        }
        else {
            gameboard.clearAllTestUiFromGameboard(context.gameboardUi);
        }
    }

    drawMinesRange(div = null) {

        let currentCell = this.playerHead;
        let cells = Helper.getValidCellsInRange(currentCell, Helper.minesRange, true);

        cells.forEach(x => {
            if (currentCell != x) {
                // mines cannot be laid on currently drawn player paths
                let isTaken = this.isDivInCurrentCoordHist($('#b'+x));
                if (isTaken === false) {
                    Helper.addRemoveClass($('#b' + x), Helper.minesRangeClass);
                }
            }
        });
    }

    static hoverMinesRange(div, event) {
        if (event.type === 'mouseenter') {

            $(div).html('<img class="power-icon-svg" src="/images/mines.svg">');
        }
        if (event.type === 'mouseleave') {
            //puts the id of the cell into the html value instead of the image that was temporarily there
            $(div).html($(div).attr('id').substring(1));
        }
    }

    static clickMinesRange(div) {

        //puts the id of the cell into the html value instead of the image that was temporarily there
        $(div).html($(div).attr('id').substring(1));
        /*
                Needs to
               
            * update model with selected power location
                * model will change styling on activating button
                * model will unhide and update power-target module
                *  make sure that the icon stays in that location
            * toggle off the power range view and button styling
         
         */
        let minesData = gameboard.powerControlsArray.find(x => x.name === jsonData.mines);

        minesData.placedCell = div.attr('id').substring(1);
        minesData.placedAddress = Helper.getCellAddress(div);
        minesData.onToggle();

        model.setActivatedPowerValue(div);
        
    }

    drawTorpedoRange() {

        let currentCell = this.playerHead;
        let cells = Helper.getValidCellsInRange(currentCell, Helper.torpedoRange);

        cells.forEach(x => {
            if (currentCell != x) {
                Helper.addRemoveClass($('#b' + x), Helper.torpedoRangeClass);
            }
        });
    }

    static hoverTorpedoRange(div, event) {
        if (event.type === 'mouseenter') {

            // TODO: URGENT!!!! Move order discrepency, currently powers are used before the move direction is calculated

            // get info for calculate angle using playerhead and cell mouse is hovering on
            let angle = Helper.calculateAngle($('#b' + gameboard.playerHead), div);

            $(div).html('<img class="power-icon-svg torpedo-map-icon" id="hover-torpedo-svg" src="/images/torpedo.svg">');

            $('#hover-torpedo-svg').css('transform', 'rotate(' + angle + 'deg)');
        }
        if (event.type === 'mouseleave') {
            $(div).html($(div).attr('id').substring(1));
        }
    }

    static clickTorpedoRange(div) {
        //puts the id of the cell into the html value instead of the image that was temporarily there
        $(div).html($(div).attr('id').substring(1));

        let torpedoData = gameboard.powerControlsArray.find(x => x.name === jsonData.torpedo);

        torpedoData.placedCell = div.attr('id').substring(1);
        torpedoData.placedAddress = Helper.getCellAddress(div);
        torpedoData.onToggle();

        model.setActivatedPowerValue(div);
    }

    drawSneakRange() {
        let currentCell = this.playerHead;
        let cells = Helper.getCellsInLine(currentCell, Helper.sneakRange);

        cells.forEach(x => {
            Helper.addRemoveClass($('#b' + x), Helper.sneakRangeClass);
        });
    }

    static hoverSneakRange(div, event) {
        if (event.type === 'mouseenter') {

            // TODO: URGENT!!!! Move order discrepency, currently powers are used before the move direction is calculated

            // get info for calculate angle using playerhead and cell mouse is hovering on
            let angle = Helper.calculateAngle($('#b' + gameboard.playerHead), div);

            $(div).html('<img class="power-icon-svg torpedo-map-icon" id="hover-sneak-svg" src="/images/sneak.svg">');

            $('#hover-sneak-svg').css('transform', 'rotate(' + angle + 'deg)');
        }
        if (event.type === 'mouseleave') {
            $(div).html($(div).attr('id').substring(1));
        }
    }

    static clickSneakRange(div) {
        //puts the id of the cell into the html value instead of the image that was temporarily there
        $(div).html($(div).attr('id').substring(1));

        let sneakData = gameboard.powerControlsArray.find(x => x.name === jsonData.sneak);

        sneakData.placedCell = div.attr('id').substring(1);
        sneakData.placedAddress = Helper.getCellAddress(div);
        sneakData.onToggle();

        model.setActivatedPowerValue(div);
    }

    drawDronesRange() {

        $('.' + Helper.dronesOverlayClass).each(function () {
            $(this).removeClass('d-none')
        });
    }

    static hoverDronesRange(div, event) {
        // done with css
    }

    static clickDroneRange(div) {
        let dronesData = gameboard.powerControlsArray.find(x => x.name === jsonData.drones);

        let clickedOverlay = $(div).find('.' + Helper.dronesOverlayClass)[0]
        let clickedOverlayId = $(clickedOverlay).attr('id').substring(1);

        dronesData.placedCell = clickedOverlayId
        dronesData.placedAddress = clickedOverlayId + 1
        dronesData.onToggle();

        model.setActivatedPowerValue(clickedOverlay)
    }

    clearAllTestUiFromGameboard(gameboardUi) {
        $('.' + gameboardUi).each(function () {
            if (gameboardUi != Helper.dronesOverlayClass) {
                $(this).removeClass(gameboardUi);
            } else {
                $(this).addClass('d-none');
            }
        });
    }

    clearAllActivatedPowerValues() {
        // TODO: need implmented urgently
        //console.log(`Test implementation of swapping activated power values`);

        // gets object that has a cellNumber and presumablly a cellAddress
        let objectToClear = gameboard.powerControlsArray.find(x => x.cellNumber != null);

        if (objectToClear != undefined) {
            objectToClear.placedCell = null;
            objectToClear.placedAddress = null;
        }
        
    }
}
