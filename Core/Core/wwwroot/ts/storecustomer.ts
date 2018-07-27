 class StoreCustomer {
    constructor(private firstName, private lastName) {

    }
    public visits: number = 0;
    private ourName = "";
    public ShowName() {
        alert(this.firstName + "--" + this.lastName);
    }
    set Name(name) {
        this.ourName = name;
    }
    get Name() {
        return this.ourName;
    }
}