class Helper {

    static powerUiSelected = 'ui-selected';
    static activatedPowerUiSelected = 'activated-ui-selected';
    static activatedPowerUiSet = 'activated-ui-set';
    static systmesUiSelected = 'ui-selected';
    static playerHeadUiSelected = 'player-head-ui-selected';
    static playerHeadUiSelectedHover = 'player-head-ui-selected-hover';
    static directionUiSelected = 'direction-ui-selected';
    static mapControlsUiSelected = 'map-controls-ui-selected';
    static placeFirstPieceUiId = '#place-first-piece';
    static enemySlideActiveUiId = '#enemy-slide-active';
    static thirdThingUiId = '#third-thing';
    static submitBtnId = '#submit-btn';
    static formId = '#form';
    static chargedPowerUnselectedText = 'r e a d y ';
    static chargedPowerSelectedText = 'a c t i v a t i n g';
    static chargedPowerConfirmedText = 's e t !';
    static nextPowerUnselectedText = '+';
    static nextPowerSelectedText = '✓';
    static playerHeadClass = 'player-head';
    static playerHeadHoverClass = 'player-head-hover';
    static showCellNumbers = '#show-cell-numbers';
    static clearCellNumbers = '#clear-cell-numbers'
    static gameboardPowerOverlayClass = 'gameboard-ui-overlay';
    static minesRangeClass = 'mines-range-ui';
    static sneakRangeClass = 'sneak-range-ui';
    static torpedoRangeClass = 'torpedo-range-ui';
    static dronesRangeClass = 'drones-range-ui';
    static torpedoRange = 4;
    static sneakRange = 4;
    static minesRange = 1;
    static modelMessagesId = '#model-messages';
    static modelMessagesListId = '#model-messages-list';
    static modelMessagesHeaderId = '#model-messages-header';
    static modelMessagesTargetId = '#model-messages-target';
    static modelMessagesCollapseId = '#model-messages-collapse-icon';
    static boxIconClass = 'box-icon'
    static boxOverlayClass = 'box-overlay'
    static boxOverlaySuffix = 'bo';
    static dronesOverlayClass = 'drones-overlay-box';
    static zoneNumberClass = 'zone-number';

    static parseCsvToArrays = (csv, needsParsed = true) => {

        if (csv == "") { return false; }

        let output = Array.from(csv.split(','));
        if (output[output.length - 1] === '') {
            output.pop();
        }

        //values that do not need to be processed further
        if (!needsParsed) { return output; }

        for (let i = 0; i < output.length; i++) {
            output[i] = JSON.parse(output[i]);
        }
        return output;
    };

    static addRemoveClass = (div, add, remove = null) => {

        if (remove != null) {
            div.removeClass(remove);
        }

        if (add === null) { return }

        div.addClass(add);
    };

    static toggleClass(div, style) {

        if (div.hasClass(style)) {
            Helper.addRemoveClass(div, null, style);
        }
        else {
            Helper.addRemoveClass(div, style);
        }
    }

    // static clearUiSelected = (uiItems) => {
    //     uiItems.forEach((item) => {
    //        let div;
    //        if (item.prevId() != null || item.prevId() != undefined) {
    //            item.suffix() === null ? div = $('#' + item.prevId()) : div = $('#' + item.suffix() + item.prevId());
    //            if (item.removeCharacter() != null) { div.html(item.removeCharacter()); }
    //            if (item.name != 'apvTarget') {
    //                if (div.hasClass(item.uiStyle())) { div.removeClass(item.uiStyle()); }
    //            }
    //        }
    //        if (item.name == 'apvTarget') {
    //            if (model.prevActivatedPowerValue != null) {
    //                div.addClass(item.uiStyle());
    //            }
    //        }
    //    });
    //};

    //static addUiSelected = (uiItems) => {
    //    uiItems.forEach((item) => {
    //        let div;
    //        if (item.id() != null || item.id() != undefined) {
    //            item.suffix() === null ? div = $('#' + item.id()) : div = $('#' + item.suffix() + item.id());
    //            if (item.addCharacter() != null) { div.html(item.addCharacter()); }
    //            if (item.name != 'apvTarget') {
    //                if (!div.hasClass(item.uiStyle())) { div.addClass(item.uiStyle()); }
    //            }
    //        }
    //        if (item.name == 'apvTarget' || item.uiStyle() == 'd-none') {
    //            if (model.activatedPowerValue != null) {
    //                div.removeClass(item.uiStyle());
    //            }
    //        }
    //    });
    //};

    static clearUiSelected = (uiItems) => {
        
        let uiGroup = [];
        let selectedSystems = uiItems.find(x => x.name === 'selectedSystems');
        let selectedDirection = uiItems.find(x => x.name === 'selectedDirection');
        let selectedPower = uiItems.find(x => x.name === 'selectedPower');
        let firstMove = uiItems.find(x => x.name === 'firstMove');
        let activatedPower = uiItems.find(x => x.name === 'activatedPower');

        uiGroup.push(selectedSystems, selectedDirection, selectedPower, firstMove, activatedPower);

        uiGroup.forEach(x => {
            if (x.prevId === null || x.prevId === undefined) { return; }
            x.remove();

            if (x.name === 'activatedPower' && model.prevActivatedPowerValue != null) {
                let prevAPV = uiItems.find(y => y.name === x.prevId);
                prevAPV.remove();
            }
        });
    };

    static addUiSelected = (uiItems) => {

        let uiGroup = [];
        let selectedSystems = uiItems.find(x => x.name === 'selectedSystems');
        let selectedDirection = uiItems.find(x => x.name === 'selectedDirection');
        let selectedPower = uiItems.find(x => x.name === 'selectedPower');
        let firstMove = uiItems.find(x => x.name === 'firstMove');
        let activatedPower = uiItems.find(x => x.name === 'activatedPower');

        uiGroup.push(selectedSystems, selectedDirection, selectedPower, firstMove, activatedPower);

        uiGroup.forEach(x => {
            if (x.id === null || x.id === undefined) { return; }
            x.add();

            if (x.name === 'activatedPower' && model.activatedPowerValue != null) {
                let APV = uiItems.find(y => y.name === x.id);
                APV.add();
                
            }
        });
    };

    static boardBoxOverlayBuilder(id) {
        return `<div class="${Helper.boxOverlayClass}" id="${id}"></div>`
    }

    static boardIconBuilder(svgName, rotateFlag = false) {

        let openTag = '<img ';
        let classTag = 'class="power-icon-svg" ';
        let sourceTag = `src="/images/${svgName}.svg" `;
        let styleTag = '';
        let idTag = `id="${svgName}-placed-icon" `;
        let closeTag = '/>'

        let playerHead = $('#b' + gameboard.playerHead);
        let targetCell = $('#b' + model.activatedPowerValue);

        if (rotateFlag == true) {
            
            let angle = Helper.calculateAngle(playerHead, targetCell);
            styleTag = `style="transform: rotate(${angle}deg)" `;
        }

        let stringOut = openTag + classTag + sourceTag + styleTag + idTag + closeTag 

        return stringOut;
    }

    // Returns an array of the cells that are around a given cell (up, down, left, and right (diagonals on with argument))
    static getValidAdjacentCells(cell, getDiagonals = false) {
        const validAdjacentCells = [];

        // convert cell to int from string
        cell = parseInt(cell, 10);

        // directions
        let westCell = cell - parseInt(1, 10);     // W: -1
        let northCell = cell - parseInt(15, 10);  // N: -15
        let southCell = cell + parseInt(15, 10); // S: +15
        let eastCell = cell + parseInt(1, 10);  // E: +1
        let northwestCell = northCell - parseInt(1, 10);     // NW: -16
        let northeastCell = northCell + parseInt(1, 10);    // NE: -14
        let southwestCell = southCell - parseInt(1, 10);   // SW: 14
        let southeastCell = southCell + parseInt(1, 10);  // SE: 16

        // elements related to directions
        let westDiv = $('#b' + westCell);
        let northDiv = $('#b' + northCell);
        let southDiv = $('#b' + southCell);
        let eastDiv = $('#b' + eastCell);
        let northwestDiv = $('#b' + northwestCell);
        let northeastDiv = $('#b' + northeastCell);
        let southwestDiv = $('#b' + southwestCell);
        let southeastDiv = $('#b' + southeastCell);

        // Check the cell to the West (left)
        if (cell % 15 > 0 && gameboard.checkIfDivIsIsland(westDiv) === false) {
            validAdjacentCells.push(westCell);
        }

        // Check the cell to the North (above)
        if (cell >= 15 && gameboard.checkIfDivIsIsland(northDiv) === false) {
            validAdjacentCells.push(northCell);
        }

        // Check the cell to the South (below)
        if (cell < 225 && gameboard.checkIfDivIsIsland(southDiv) === false) {
            validAdjacentCells.push(southCell);
        }

        // Check the cell to the East (right)
        if (cell % 15 < 14 && gameboard.checkIfDivIsIsland(eastDiv) === false) {
            validAdjacentCells.push(eastCell);
        }

        if (getDiagonals) {
            // Check the cell to the Northwest (top-left)
            if (cell % 15 > 0 && cell >= 15 && gameboard.checkIfDivIsIsland(northwestDiv) === false) {
                validAdjacentCells.push(northwestCell);
            }

            // Check the cell to the Northeast (top-right)
            if (cell % 15 < 14 && cell >= 15 && gameboard.checkIfDivIsIsland(northeastDiv) === false) {
                validAdjacentCells.push(northeastCell);
            }

            // Check the cell to the Southwest (bottom-left)
            if (cell % 15 > 0 && cell < 225 && gameboard.checkIfDivIsIsland(southwestDiv) === false) {
                validAdjacentCells.push(southwestCell);
            }

            // Check the cell to the Southeast (bottom-right)
            if (cell % 15 < 14 && cell < 225 && gameboard.checkIfDivIsIsland(southeastDiv) === false) {
                validAdjacentCells.push(southeastCell);
            }
        }

        return validAdjacentCells;
    }

    // Returns an array of all cells that are within a certain number of steps of the given cell that are valid
    static getValidCellsInRange(cell, steps, getDiagonals = false) {

        const validCellsInRange = [cell];

        for (let i = 0; i < steps; i++) {
            const newCellsInRange = [];

            for (const c of validCellsInRange) {
                const adjacentCells = this.getValidAdjacentCells(c, getDiagonals);
                for (const ac of adjacentCells) {
                    if (!validCellsInRange.includes(ac) && !newCellsInRange.includes(ac)) {
                        newCellsInRange.push(ac);
                    }
                }
            }

            validCellsInRange.push(...newCellsInRange);
        }

        return validCellsInRange;
    }

    static getCellsInLine(cell, steps) {

        //output format, weast cells, north cells, south cells, east cells
        const validCellsInRange = [[], [], [], []];
        let output = [];
        cell = parseInt(cell, 10);

        // outer loop manages the number of steps that will be taken
        for (let i = 0; i < steps; i++) {

            //inner loop goes through the valid cells in range array,
            //  giving each internal array a turn
            for (let j = 0; j < validCellsInRange.length; j++) {

                let startPoint;
                let topIndex = validCellsInRange[j].length - 1;
                let newCell;
                let newDiv;

                // if the top of the array is empty, start from where the player head is
                if (validCellsInRange[j][topIndex] == undefined ) {
                    startPoint = cell
                } else {
                    startPoint = validCellsInRange[j][topIndex]
                }

                // different calculations for the different directions
                switch (j) {
                    case 0: //moving west
                        newCell = startPoint - parseInt(1, 10);
                        newDiv = $('#b' + newCell);
                        if (startPoint % 15 > 0 &&
                            gameboard.checkIfDivIsIsland(newDiv) === false &&
                            gameboard.isDivInCurrentCoordHist(newDiv) === false) {
                            validCellsInRange[j].push(newCell);
                            output.push(newCell);
                        }
                        break;
                    case 1: //moving north
                        newCell = startPoint - parseInt(15, 10);
                        newDiv = $('#b' + newCell);
                        if (startPoint >= 15 &&
                            gameboard.checkIfDivIsIsland(newDiv) === false &&
                            gameboard.isDivInCurrentCoordHist(newDiv) === false) {
                            validCellsInRange[j].push(newCell);
                            output.push(newCell);
                        }
                        break;
                    case 2: //moving south
                        newCell = startPoint + parseInt(15, 10);
                        newDiv = $('#b' + newCell);
                        if (startPoint < 225 &&
                            gameboard.checkIfDivIsIsland(newDiv) === false &&
                            gameboard.isDivInCurrentCoordHist(newDiv) === false) {
                            validCellsInRange[j].push(newCell);
                            output.push(newCell);
                        }
                        break;
                    case 3: //moving east
                        newCell = startPoint + parseInt(1, 10);
                        newDiv = $('#b' + newCell);
                        if (startPoint % 15 < 14 &&
                            gameboard.checkIfDivIsIsland(newDiv) === false &&
                            gameboard.isDivInCurrentCoordHist(newDiv) === false) {
                            validCellsInRange[j].push(newCell);
                            output.push(newCell);
                        }
                        break;
                    default:
                        break;
                }
            }
        }
        return output;
    }

    static calculateAngle(div1, div2) {

        let oldCoords = Helper.getXYCoords(div1);
        let newCoords = Helper.getXYCoords(div2);

        let deltaX = newCoords.x - oldCoords.x;
        let deltaY = newCoords.y - oldCoords.y;

        // Added '+ 90'' to correct angle
        return (Math.atan2(deltaY, deltaX) * 180 / Math.PI) + 90
    }

    static getCellAddress(div) {
        let coords = Helper.getXYCoords(div);
        coords.x = String.fromCharCode(coords.x + 65);

        // String of Coordinates
        return coords.x + parseInt(coords.y + 1, 10);
    }

    static getXYCoords(div) {

        let coords = {
            x: Helper.getRowNumber(div),
            y: Helper.getColNumber(div)
        }

        return coords;
    }

    static getRowNumber(div) {
        let cell = div.attr('id').substring(1);
        let row = cell - Math.floor((cell / jsonData.mapLength)) * jsonData.mapLength;
        return row;
    }

    static getColNumber(div) {
        let cell = div.attr('id').substring(1);
        let col = Math.floor((cell / jsonData.mapWidth));
        return col;
    }

    static getDivById(id, suffix = null) {
        let div;
        suffix === null ? div = $(`#${id}`) : div = $(`#${suffix}${id}`); 
        return div;
    }
}