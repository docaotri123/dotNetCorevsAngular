var StoreCustomer = /** @class */ (function () {
    function StoreCustomer(firstName, lastName) {
        this.firstName = firstName;
        this.lastName = lastName;
        this.visits = 0;
        this.ourName = "";
    }
    StoreCustomer.prototype.ShowName = function () {
        alert(this.firstName + "--" + this.lastName);
    };
    Object.defineProperty(StoreCustomer.prototype, "Name", {
        get: function () {
            return this.ourName;
        },
        set: function (name) {
            this.ourName = name;
        },
        enumerable: true,
        configurable: true
    });
    return StoreCustomer;
}());
//# sourceMappingURL=storecustomer.js.map