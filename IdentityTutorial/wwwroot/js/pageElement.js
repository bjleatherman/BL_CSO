class PageElement {
    constructor(name, suffix, id, uiStyle, addHtml, removeHtml, childOf = null, prevId = null, prevAddHtml = null, prevRemoveHtml = null) {
        this.name = name;
        this.suffix = suffix;
        this.id = [];
        Array.isArray(id) ? this.id = id : this.id.push(id);
        this.uiStyle = uiStyle;
        this.addHtml = addHtml;
        this.removeHtml = removeHtml;
        this.childOf = childOf;

        if (prevId != null) {
            this.prevId = [];
            Array.isArray(prevId) ? this.prevId = prevId : this.prevId.push(prevId);
        }
        
    }

    // Accessors and Mutators
    getName() {
        return this.name;
    }
    setName(name) {
        this.name = name;
    }
    getSuffix() {
        return this.suffix;
    }
    setSuffix(suffix) {
        this.suffix = suffix
    }
    getId() {
        return this.id
    }
    setId(id) {
        this.prevId = this.id;
        Array.isArray(id) ? this.id = id : this.id = [id];
    }
    getUiStyle() {
        return this.uiStyle;
    }
    setUiStyle(uiStyle) {
        this.uiStyle = uiStyle;
    }
    getAddHtml() {
        return this.addHtml;
    }
    setAddHtml(addHtml) {
        this.addHtml = addHtml;
    }
    getRemoveHtml() {
        return this.removeHtml;
    }
    setRemoveHtml(removeHtml) {
        this.removeHtml = removeHtml;
    }
    getPrevId() {
        return this.prevId
    }
    setPrevId(prevId) {
        this.prevId = prevId;
    }
    getPrevAddHtml() {
        return this.prevAddHtml;
    }
    setPrevAddHtml(prevAddHtml) {
        this.prevAddHtml = prevAddHtml;
    }
    getPrevRemoveHtml() {
        return this.removeHtml;
    }
    setPrevRemoveHtml(prevRemoveHtml) {
        this.prevRemoveHtml = prevRemoveHtml;
    }

    // Functions
    buildDiv(int) {
        let id;
        this.suffix != null ? id = '#' + this.suffix + int : id = '#' + int;
        let div = $(id); 
        return div;
    }

    drawElement() {
        console.log(`in drawElement`);
        this.id.forEach(x => {
            let div = this.buildDiv(x)
            div.addClass(this.uiStyle);
            div.html(this.addHtml);
        });
    }

    eraseElement() {

        this.prevId = this.id;

        console.log(`in eraseElement`);
        this.prevId.forEach(x => {
            let div = this.buildDiv(x);
            div.removeClass(this.uiStyle);
            div.html(this.removeHtml)
        });
    }
}