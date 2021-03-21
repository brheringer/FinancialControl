import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

import { EntitiesReferences } from '../core/model/entities-references';
import { GenericService } from '../core/services/generic.service';

import { Importing } from '../model/importing.model';

@Injectable()
export class ImportingService {

  uri = 'Importing';

  constructor(private generic: GenericService) { }

  import(file: any): Observable<Importing> {
    return this.generic.post<Importing>(this.uri, file);
  }

}
