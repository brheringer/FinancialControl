import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { EntitiesReferences } from '../core/model/entities-references';
import { GenericService } from '../core/services/generic.service';

import { MemoMapping } from '../model/memo-mapping.model';
import { MemosMappings } from '../model/memos-mappings.model';

@Injectable()
export class MemoMappingService {

  uri = 'MemoMapping';

  constructor(private generic: GenericService) { }

  delete(id: number): Observable<MemoMapping>
  {
    return this.generic.delete<MemoMapping>(this.uri, id);
  }

  get(id: number): Observable<MemoMapping> {
    return this.generic.get<MemoMapping>(this.uri, id);
  }

  loadAllAsReferences(): Observable<EntitiesReferences>
  {
    return this.generic.getAll<EntitiesReferences>(`${this.uri}/all`);
  }

  search(dto: MemosMappings): Observable<MemosMappings> {
    return this.generic.post<MemosMappings>(`${this.uri}/search`, dto);
  }

  update(dto: MemoMapping): Observable<MemoMapping> {
    return this.generic.post<MemoMapping>(this.uri, dto);
  }

}
