


function Queue() {
    this.dataStore = [];
    this.enqueue = enqueue;
    this.dequeue = dequeue;
    this.front = front;
    this.back = back;
    this.toString = toString;
    this.empty = empty;
}

//enqueue
//ť�� ���κп� ��Ҹ� �߰�
function enqueue(element) {
    this.dataStore.push(element);
}

//dequeue
//ť�� �պκп��� ��Ҹ� ����
function dequeue() {
    return this.dataStore.shift();
}

//front
//ť�� �պκп� ����� ��� Ȯ��
function front() {
    return this.dataStore[0];
}

//back
//ť�� ���κп� ����� ��� Ȯ��
function back() {
    return this.dataStore[this.dataStore.length - 1];
}

//toString
//ť�� ��� ��Ҹ� ���
function toString() {
    var retStr = "";
    for (var i = 0; i < this.dataStore.length; ++i) {
        retStr += this.dataStore[i] + "\n";
    }
    return retStr;
}

//empty
//ť�� ����ִ��� ���� Ȯ��
function empty() {
    if (this.dataStore.length == 0) {
        return true;
    } else {
        return false;
    }
}

exports.Queue = Queue;