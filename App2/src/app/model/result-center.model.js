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
var Entity_1 = require("../core/model/Entity");
var entityStatus_model_1 = require("./entityStatus.model");
var ResultCenter = /** @class */ (function (_super) {
    __extends(ResultCenter, _super);
    function ResultCenter() {
        var _this = _super !== null && _super.apply(this, arguments) || this;
        _this.status = new entityStatus_model_1.EntityStatus();
        return _this;
    }
    return ResultCenter;
}(Entity_1.Entity));
exports.ResultCenter = ResultCenter;
//# sourceMappingURL=result-center.model.js.map