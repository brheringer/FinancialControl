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
var data_transfer_object_1 = require("../core/model/data-transfer-object");
var AccountsTotalizationsReport = /** @class */ (function (_super) {
    __extends(AccountsTotalizationsReport, _super);
    function AccountsTotalizationsReport() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.accountsTotalizations = new Array();
        return _this;
    }
    return AccountsTotalizationsReport;
}(data_transfer_object_1.DataTransferObject));
exports.AccountsTotalizationsReport = AccountsTotalizationsReport;
//# sourceMappingURL=accounts-totalizations-report.model.js.map