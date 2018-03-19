"use strict";
var __extends = (this && this.__extends) || (function () {
    var extendStatics = Object.setPrototypeOf ||
        ({ __proto__: [] } instanceof Array && function (d, b) { d.__proto__ = b; }) ||
        function (d, b) { for (var p in b) if (b.hasOwnProperty(p)) d[p] = b[p]; };
    return function (d, b) {
        extendStatics(d, b);
        function __() { this.constructor = d; }
        d.prototype = b === null ? Object.create(b) : (__.prototype = b.prototype, new __());
    };
})();
Object.defineProperty(exports, "__esModule", { value: true });
var collection_1 = require("../core/model/collection");
var Accounts = /** @class */ (function (_super) {
    __extends(Accounts, _super);
    function Accounts() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.accounts = new Array();
        return _this;
    }
    return Accounts;
}(collection_1.Collection));
exports.Accounts = Accounts;
//# sourceMappingURL=accounts.model.js.map