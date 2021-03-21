import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { EntitiesReferences } from '../core/model/entities-references';
import { GenericService } from '../core/services/generic.service';

import { Entry } from '../model/entry.model';
import { Entries } from '../model/entries.model';

@Injectable()
export class EntryService {

  uri = 'Entry';

  constructor(private generic: GenericService) { }

  delete(id: number): Observable<Entry>
  {
    return this.generic.delete<Entry>(this.uri, id);
  }

  get(id: number): Observable<Entry> {
    return this.generic.get<Entry>(this.uri, id);
  }

  search(dto: Entries): Observable<Entries> {
    return this.generic.post<Entries>(`${this.uri}/search`, dto);
  }

  update(dto: Entry): Observable<Entry> {
    return this.generic.post<Entry>(this.uri, dto);
  }

}
