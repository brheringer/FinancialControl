import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { GenericService } from './generic.service';
import { SessionService } from '../local-services/session.service';

import { User } from '../model/user';
import { UserSession } from '../model/user-session';

@Injectable()
export class LoginService {

  constructor(private generic: GenericService, private session: SessionService) { }

  login(dto: User): Observable<UserSession>
  {
    return new Observable(observer => {
      this.generic.post<UserSession>('login', dto).subscribe(sessionDto => {
        if (sessionDto && sessionDto.userSessionToken) {
          this.session.setCurrentSession(sessionDto);
        }
        observer.next(sessionDto);
        observer.complete();
      })
    });
    // let observable = this.generic.post<UserSession>('login', dto);
    // observable.subscribe(sessionDto => {
    //   if (sessionDto && sessionDto.userSessionToken) {
    //     this.session.setCurrentSession(sessionDto);
    //   }
    // });
    // return observable;
  }

  logout(): void
  {
    //TODO logout no servidor?
    this.session.clear();
  }

  getUser(id: number): Observable<User> {
    return this.generic.get<User>('login/user', id);
  }
}
