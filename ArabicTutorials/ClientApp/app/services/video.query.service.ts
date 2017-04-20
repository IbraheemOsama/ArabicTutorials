import { Injectable } from '@angular/core';
import { Http, Response, Headers } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import { ConfigurationService } from './configuration.service';
import { BaseDetails } from '../models';

@Injectable()
export class VideoQueryService {

    constructor(private http: Http, private configurationService: ConfigurationService) {

    }

    public getAll(): Observable<BaseDetails[]> {
        console.log(`${this.configurationService.queryServiceUrl}api/Videos/GetAll`);
        return this.http.get(`${this.configurationService.queryServiceUrl}api/Videos/GetAll`).map(this.extractData);
    }

    //public create(book: Book): Observable<Book> {
    //    var headers = new Headers({
    //        'Content-Type': 'application/json'
    //    });
    //    return this.http.post('http://localhost:61736/api/books', JSON.stringify(book), { headers: headers }).map(this.extractData);
    //}

    //public delete(book: Book) {
    //    return this.http.delete(`http://localhost:61736/api/books/${book.id}`);
    //}

    private extractData(res: Response) {
        let body = res.json();
        console.log(body);
        return body || {};
    }
}