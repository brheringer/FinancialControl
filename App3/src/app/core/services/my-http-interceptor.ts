import { Injectable } from '@angular/core';
import { HttpEvent, HttpResponse, HttpErrorResponse, HttpInterceptor, HttpHandler, HttpRequest } from '@angular/common/http';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { DataTransferObject } from '../model/data-transfer-object';
import { Response } from '../model/response';
import { AlertService } from '../local-services/alert.service';

@Injectable()
export class MyHttpInterceptor implements HttpInterceptor {

  constructor(private alertService: AlertService) { }

  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next
      .handle(req)
      .pipe(catchError((err, caught) => {
        if (err instanceof HttpErrorResponse) {
          let dto = this.dealWithLowLevelError(err);
          return caught;
        }
        else {
          return Observable.throw(err);
        }
      }));
  }

  dealWithLowLevelError(err: HttpErrorResponse) {
    let genericErrorMessageDto = new DataTransferObject();
    genericErrorMessageDto.response = new Response();
    genericErrorMessageDto.response.hasException = true;

    if (err.error instanceof Error) {
      // A client-side or network error occurred. Handle it accordingly.
      this.Log(err.error.message);
      genericErrorMessageDto.response.exception = "Uma operação não foi concluída com sucesso. Consulte o log.";
    } else {
      // The backend returned an unsuccessful response code.
      // The response body may contain clues as to what went wrong,
      this.Log('[' + err.status + '] ' + err.message);
      genericErrorMessageDto.response.exception = "[${err.status}] Uma operação não foi concluída com sucesso. Consulte o log.";
    }
    return genericErrorMessageDto;
  }

  Log(msg) {
    this.alertService.error('interceptor: ' + msg); //TODO nao gostei de usar isso aqui mas nao consegui fazer de outro jeito
    console.log(msg);
  }
}

/*
The err parameter to the callback above is of type HttpErrorResponse, and contains useful information on what went wrong.
There are two types of errors that can occur. If the backend returns an unsuccessful response code (404, 500, etc.), it gets returned as an error. Also, if something goes wrong client-side, such as an exception gets thrown in an RxJS operator, or if a network error prevents the request from completing successfully, an actual Error will be thrown.
In both cases, you can look at the HttpErrorResponse to figure out what happened.
*/
