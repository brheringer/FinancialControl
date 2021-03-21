import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { GenericService } from '../core/services/generic.service';

import { AccountsTotalizationsReport } from '../model/accounts-totalizations-report.model';

@Injectable()
export class AccountsTotalizationsReportService {

  uri = 'AccountsTotalizationsReport';

  constructor(private generic: GenericService) { }

  generateReport(dto: AccountsTotalizationsReport): Observable<AccountsTotalizationsReport> {
    return this.generic.post<AccountsTotalizationsReport>(this.uri, dto);
  }

}
