import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';

import { EntitiesReferences } from '../core/model/entities-references';
import { GenericService } from '../core/services/generic.service';

import { EntryTemplate } from '../model/entry-template.model';
import { EntriesTemplates } from '../model/entries-templates.model';

@Injectable()
export class EntryTemplateService {

  uri = 'EntryTemplate';

  constructor(private generic: GenericService) { }

  delete(id: number): Observable<EntryTemplate>
  {
    return this.generic.delete<EntryTemplate>(this.uri, id);
  }

  get(id: number): Observable<EntryTemplate> {
    return this.generic.get<EntryTemplate>(this.uri, id);
  }

  getAll(): Observable<EntriesTemplates> {
    return this.search(new EntriesTemplates());
  }

  loadAllAsReferences(): Observable<EntitiesReferences>
  {
    return this.generic.getAll<EntitiesReferences>(`${this.uri}/all`);
  }

  search(dto: EntriesTemplates): Observable<EntriesTemplates> {
    return this.generic.post<EntriesTemplates>(`${this.uri}/search`, dto);
  }

  update(dto: EntryTemplate): Observable<EntryTemplate> {
    return this.generic.post<EntryTemplate>(this.uri, dto);
  }

}
