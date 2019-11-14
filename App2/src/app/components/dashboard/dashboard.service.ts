import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { GenericService } from '../../core/services/generic.service';

import { Dashboard } from './dashboard.model';

@Injectable()
export class DashboardService {

  uri = 'dashboard';

  constructor(private generic: GenericService) { }

  retrieveEntries(dto: Dashboard): Observable<Dashboard> {
    return this.generic.post<Dashboard>(this.uri, dto);
  }

}
