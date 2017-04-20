"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
require("rxjs/add/operator/map");
var configuration_service_1 = require("./configuration.service");
var VideoQueryService = (function () {
    function VideoQueryService(http, configurationService) {
        this.http = http;
        this.configurationService = configurationService;
    }
    VideoQueryService.prototype.getAll = function () {
        console.log(this.configurationService.queryServiceUrl + "api/Videos/GetAll");
        return this.http.get(this.configurationService.queryServiceUrl + "api/Videos/GetAll").map(this.extractData);
    };
    //public create(book: Book): Observable<Book> {
    //    var headers = new Headers({
    //        'Content-Type': 'application/json'
    //    });
    //    return this.http.post('http://localhost:61736/api/books', JSON.stringify(book), { headers: headers }).map(this.extractData);
    //}
    //public delete(book: Book) {
    //    return this.http.delete(`http://localhost:61736/api/books/${book.id}`);
    //}
    VideoQueryService.prototype.extractData = function (res) {
        var body = res.json();
        console.log(body);
        return body || {};
    };
    return VideoQueryService;
}());
VideoQueryService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http, configuration_service_1.ConfigurationService])
], VideoQueryService);
exports.VideoQueryService = VideoQueryService;
//# sourceMappingURL=video.query.service.js.map