import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { EntitiesReferences } from '../core/model/entities-references';
import { GenericService } from '../core/services/generic.service';

import { ResultCenter } from '../model/result-center.model';
import { ResultsCenters } from '../model/results-centers.model';

@Injectable()
export class ResultCenterService {

  uri = 'ResultCenter';

  constructor(private generic: GenericService) { }

  delete(id: number): Observable<ResultCenter>
  {
    return this.generic.delete<ResultCenter>(this.uri, id);
  }

  get(id: number): Observable<ResultCenter> {
    return this.generic.get<ResultCenter>(this.uri, id);
  }

  loadAllAsReferences(): Observable<EntitiesReferences>
  {
    return this.generic.getAll<EntitiesReferences>(`${this.uri}/all`);
  }

  search(dto: ResultsCenters): Observable<ResultsCenters> {
    return this.generic.post<ResultsCenters>(`${this.uri}/search`, dto);
  }

  update(dto: ResultCenter): Observable<ResultCenter> {
    return this.generic.post<ResultCenter>(this.uri, dto);
  }

}
