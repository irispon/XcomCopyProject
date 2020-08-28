
class Queue {
    constructor() {
        this._arr = [];
    }
    enqueue(item) {
        this._arr.push(item);
    }
    dequeue() {
        return this._arr.shift();
    }

    toString() {
    var retStr = "";
        for (var i = 0; i < this._arr.length; ++i) {
        retStr += this._arr[i] + "\n";
    }
    return retStr;
    }

    empty() {
    if (this._arr.length == 0) {
        return true;
    } else {
        return false;
    }
}

}





module.exports = Queue;