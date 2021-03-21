"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var EntityStatus = /** @class */ (function () {
    function EntityStatus() {
        this.error = false;
        this.msg = '';
    }
    EntityStatus.prototype.setError = function (msg) {
        this.msg = msg;
        this.error = true;
    };
    EntityStatus.prototype.setNonError = function (msg) {
        this.msg = msg;
        this.error = false;
    };
    return EntityStatus;
}());
exports.EntityStatus = EntityStatus;
//# sourceMappingURL=entityStatus.model.js.map