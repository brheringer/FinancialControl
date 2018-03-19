import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { EntitiesReferences } from '../core/model/entities-references';
import { GenericService } from '../core/services/generic.service';

import { Account } from '../model/account.model';
import { Accounts } from '../model/accounts.model';

@Injectable()
export class AccountService {

  uri = 'Account';

  constructor(private generic: GenericService) { }

  delete(id: number): Observable<Account>
  {
    return this.generic.delete<Account>(this.uri, id);
  }

  get(id: number): Observable<Account> {
    return this.generic.get<Account>(this.uri, id);
  }

  loadAllAsReferences(): Observable<EntitiesReferences>
  {
    return this.generic.getAll<EntitiesReferences>(`${this.uri}/all`);
  }

  search(dto: Accounts): Observable<Accounts> {
    return this.generic.post<Accounts>(`${this.uri}/search`, dto);
  }

  update(dto: Account): Observable<Account> {
    return this.generic.post<Account>(this.uri, dto);
  }

}
