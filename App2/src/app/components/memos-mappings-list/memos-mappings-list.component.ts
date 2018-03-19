import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { Subscription } from 'rxjs/Subscription';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';

import { AlertService } from '../../core/local-services/alert.service';
import { MemoMapping } from '../../model/memo-mapping.model';
import { MemosMappings } from '../../model/memos-mappings.model';
import { EntryType } from '../../model/entryType.model';
import { EntityStatus } from '../../model/entityStatus.model';
import { MemoMappingService } from '../../services/memo-mapping.service';

@Component({
  selector: 'memos-mappings-list',
  templateUrl: './memos-mappings-list.component.html',
  styleUrls: ['./memos-mappings-list.component.css'],
  providers: [MemoMappingService]
})
export class MemosMappingsListComponent implements OnInit {

  public model = new MemosMappings();
  public showFilters = true;
  public searchAsYouTypeSubject = new Subject();
  public searchAsYouTypeObservable = new Subscription();
  public types = EntryType.All;

  constructor(
    private service: MemoMappingService,
    private alertService: AlertService,
    private router: Router) { }

  ngOnInit() {
    this.search();

    this.searchAsYouTypeObservable = this.searchAsYouTypeSubject
      .debounceTime(300)
      .subscribe(() => {
        this.search();
      })
  }

  searchAsYouType(): void {
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
          this.model.mappings = dto.mappings; //refresh
          this.sort();
          this.alertService.success('Done searching.');
        }
      });
  }

  sort() {
    this.model.mappings.sort((a, b) => {
      return a.incomingMemo < b.incomingMemo ? -1 : 1
    });
  }

  createNew(): void {
    let mapping = new MemoMapping();
    this.model.mappings.push(mapping);
  }

  delete(mapping: MemoMapping, index: number): void {
    if (mapping.autoId > 0) {
      this.service.delete(mapping.autoId).subscribe(dto => {
        if (dto.response.hasException) {
          this.alertService.error(dto.response.exception);
          mapping.status.setError(dto.response.exception);
        }
        else {
          this.model.mappings.splice(index, 1);
          this.alertService.success('Delete ok!');
        }
      });
    }
    else {
      this.model.mappings.splice(index, 1);
    }
  }

  saveIfValid(mapping: MemoMapping): void {
    if (this.isValid(mapping))
      this.save(mapping);
  }

  isValid(mapping: MemoMapping): boolean {
    //sera que nao da pra resolver isso com form validation?
    return mapping
      && mapping !== null
      && mapping.incomingMemo
      && mapping.incomingMemo !== null
      && mapping.incomingMemo != '';
  }

  save(mapping: MemoMapping): void {
    this.service.update(mapping).subscribe(dto => {
      if (dto.response.hasException) {
        this.alertService.error(dto.response.exception);
        mapping.status.setError(dto.response.exception);
      }
      else {
        this.alertService.success('Update ok!');
        this.refresh(dto, mapping);
        mapping.status.setNonError('Update @ ' + new Date());
      }
    });
  }

  refresh(from: MemoMapping, to: MemoMapping) {
    to.autoId = from.autoId;
    to.creationDateTime = from.creationDateTime;
    to.creationUser = from.creationUser;
    to.incomingMemo = from.incomingMemo;
    to.lastUpdateDateTime = from.lastUpdateDateTime;
    to.lastUpdateUser = from.lastUpdateUser;
    to.mappedMemo = from.mappedMemo;
    to.presentation = from.presentation;
    to.mappedAccount = from.mappedAccount;
    to.mappedCenter = from.mappedCenter;
    to.user = from.user;
    to.version = from.version;
    to.status = new EntityStatus();
  }

}

//TODO salvar somente se realmente teve modificacao
//TODO smart-search ainda cheio de problemas
