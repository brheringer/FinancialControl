import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpClient, HttpErrorResponse, HttpHeaders, HttpParams } from '@angular/common/http';
import { DataTransferObject } from '../model/data-transfer-object';
import { Response } from '../model/response';
import { SessionService } from '../local-services/session.service';
import { EntitiesReferences } from '../model/entities-references';
import config from "../../../assets/config/financial-control-config.json";

@Injectable()
export class GenericService {

  baseUrl = 'http://localhost:58452/api/'; //TODO config.servicesBaseUrl; //nao sei pq deu pau
  
  constructor(private http: HttpClient, private session: SessionService) { }

  get<T>(url: string, id: number): Observable<T>
  {
    let params = new HttpParams().set('id', id.toString());
    let options = { headers: this.getHeaders(), params: params, withCredentials: true };
    return this.http.get<T>(this.getFullUrl(url), options);
  }

  getAll<T>(url: string): Observable<T> {
    let options = { headers: this.getHeaders(), withCredentials: true };
    return this.http.get<T>(this.getFullUrl(url), options);
  }

  smartSearch(targetService: string, smartEntry: string): Observable<EntitiesReferences> {
    let url = targetService + '/smartSearch';
    return this.post<EntitiesReferences>(url, { 'smartEntry': smartEntry });
  }

  post<T>(url: string, dto: any): Observable<T>
  {
    let options = { headers: this.getHeaders() };
    return this.http.post<T>(this.getFullUrl(url), dto, options);
  }

  delete<T>(url: string, id: number): Observable<T> {
    let params = new HttpParams().set('id', id.toString());
    let options = { headers: this.getHeaders(), params: params, withCredentials: true };
    return this.http.delete<T>(this.getFullUrl(url), options);
  }

  getText(url: string): Observable<string>
  {
    let headers = this.getHeaders()
    headers.append('responseType', 'text');
    let options = { headers: headers };
    return this.http.get<string>(this.getFullUrl(url), options );
  }

  getHeaders(): HttpHeaders
  {
    return new HttpHeaders()
      .set('UserName', this.getUserName())
      .set('SessionId', this.getToken());
  }

  private getUserName(): string
  {
    if (this.session.hasCurrentSession()) {
      let currentSession = this.session.getCurrentSession();
      return currentSession.userName;
    }
    else {
      return '';
    }
  }

  private getToken(): string
  {
    if (this.session.hasCurrentSession()) {
      let currentSession = this.session.getCurrentSession();
      return currentSession.userSessionToken;
    }
    else {
      return '';
    }
  }

  getFullUrl(url: string)
  {
    return this.baseUrl + url; //TODO mais roubstez
  }
}

