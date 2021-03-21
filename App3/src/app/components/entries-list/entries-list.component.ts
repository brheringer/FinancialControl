import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { Subscription } from 'rxjs';
import { debounceTime } from 'rxjs/operators';
import * as moment from 'moment';

import { AlertService } from '../../core/local-services/alert.service';
import { EntityReference } from '../../core/model/entity-reference';
import { Entry } from '../../model/entry.model';
import { Entries } from '../../model/entries.model';
import { EntryTemplate } from '../../model/entry-template.model';
import { EntityStatus } from '../../model/entityStatus.model';
import { Account } from '../../model/account.model';
import { ResultCenter } from '../../model/result-center.model';
import { EntryService } from '../../services/entry.service';
import { EntryTemplateService } from '../../services/entry-template.service';

@Component({
  selector: 'entries-list',
  templateUrl: './entries-list.component.html',
  styleUrls: ['./entries-list.component.css'],
  providers: [EntryService, EntryTemplateService]
})
export class EntriesListComponent implements OnInit {

  public model = new Entries();
  public showFilters = true;
  public searchAsYouTypeSubject = new Subject();
  public searchAsYouTypeObservable = new Subscription();
  public templates = new Array<EntryTemplate>();
  public layoutMode = 'GRID2';

  constructor(
    private service: EntryService,
    private entryTemplateService: EntryTemplateService,
    private alertService: AlertService,
    private router: Router) { }

  ngOnInit() {
    this.entryTemplateService.getAll()
      .subscribe(dto => {
        if (dto.response.hasException)
          this.alertService.error(dto.response.exception);
        else
          this.templates = dto.templates;
      });

    this.searchAsYouTypeObservable = this.searchAsYouTypeSubject
      .pipe(debounceTime(300))
      .subscribe(() => {
        this.search();
      })
  }

  nextPage(): void {
    this.model.searchPage++;
    this.search();
  }

  previousPage(): void {
    if(this.model.searchPage > 1) {
      this.model.searchPage--;
      this.search();
    }
  }

  searchAsYouType(): void {
    this.model.searchPage = 1;
    this.searchAsYouTypeSubject.next();
  }

  search(): void {
    this.alertService.success('Searching...');
    this.service.search(this.model)
      .subscribe(dto => {
        if (dto.response.hasException) {
          this.alertService.error(dto.response.exception);
        }
        else {
          this.model.entries = dto.entries; //refresh
          this.sort();
          this.alertService.success('Done searching.');
        }
      });
  }

  sort() {
    this.model.entries.sort((a, b) => {
      return a.date < b.date ? 1 : -1
    });
  }

  createNew(): void {
    let e = new Entry();
    e.date = moment.utc().startOf("day").toDate();
    e.account = new Account();
    e.center = new ResultCenter();
    this.model.entries.push(e);
  }

  createNewFromTemplate(template: EntryTemplate): void {
    let e = new Entry();
    e.date = moment.utc().startOf("day").toDate();
    e.account = this.clone(template.account);
    e.center = this.clone(template.center);
    e.memo = template.memo;
    e.value = template.value;
    this.model.entries.push(e);
  }

  delete(entry: Entry, index: number): void {
    if (entry.autoId > 0) {
      this.service.delete(entry.autoId).subscribe(dto => {
        if (dto.response.hasException) {
          this.alertService.error(dto.response.exception);
          entry.status.setError(dto.response.exception);
        }
        else {
          this.model.entries.splice(index, 1);
          this.alertService.success('Delete ok!');
        }
      });
    }
    else {
      this.model.entries.splice(index, 1);
    }
  }

  duplicate(entry: Entry, index: number): void {
    let copy = new Entry();
    copy.account = this.clone(entry.account);
    copy.center = this.clone(entry.center);
    copy.date = entry.date;
    copy.value = entry.value;
    copy.memo = entry.memo;
    this.model.entries.splice(index + 1, 0, copy);
  }

  clone(original: EntityReference): EntityReference {
    let clone = new EntityReference;
    clone.autoId = original.autoId;
    clone.presentation = original.presentation;
    return clone;
  }


  saveIfValid(entry: Entry): void {
    if (this.isValid(entry))
      this.save(entry);
  }

  isValid(entry: Entry): boolean {
    //sera que nao da pra resolver isso com form validation?
    return entry
      && entry !== null
      && entry.date
      && entry.date !== null
      && entry.account
      && entry.account !== null
      && entry.account.autoId > 0
      && entry.center
      && entry.center !== null
      && entry.center.autoId > 0
      && entry.memo
      && entry.memo !== null
      && entry.memo != ''
      && entry.value
      && entry.value !== null;
  }

  save(entry: Entry): void {
    this.service.update(entry).subscribe(dto => {
      if (dto.response.hasException) {
        //this.alertService.error(dto.response.exception);
        entry.status.setError(dto.response.exception);
      }
      else {
        //this.alertService.success('Update ok!');
        this.refresh(dto, entry);
        entry.status.setNonError('Update @ ' + new Date());
      }
    });
  }

  refresh(from: Entry, to: Entry) {
    to.autoId = from.autoId;
    to.creationDateTime = from.creationDateTime;
    to.creationUser = from.creationUser;
    to.lastUpdateDateTime = from.lastUpdateDateTime;
    to.lastUpdateUser = from.lastUpdateUser;
    to.presentation = from.presentation;
    to.version = from.version;
    to.account = from.account;
    to.center = from.center;
    to.date = from.date;
    to.memo = from.memo;
    to.user = from.user;
    to.value = from.value;
    to.status = new EntityStatus();
  }

}

//TODO salvar somente se realmente teve modificacao
//TODO se clicar nos botoes pra aumentar/diminuir o valor, nao salva
//TODO se der erro no interceptor, mostra alerta geral, mas não mostra que item não foi salvo, isso é problema?
//TODO delete deu pau ( [405] Http failure response for http://localhost:58452/api/Entry?id=2: 405 Method Not Allowed )
//TODO confirmar exclusao
//TODO permitir ordenar por qualquer coluna
//TODO mostar total e contagem
//TODO exportar csv?
