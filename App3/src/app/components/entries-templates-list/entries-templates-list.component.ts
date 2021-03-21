import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { Subscription } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

import { AlertService } from '../../core/local-services/alert.service';
import { EntryTemplate } from '../../model/entry-template.model';
import { EntriesTemplates } from '../../model/entries-templates.model';
import { EntryType } from '../../model/entryType.model';
import { EntityStatus } from '../../model/entityStatus.model';
import { EntryTemplateService } from '../../services/entry-template.service';

@Component({
  selector: 'entries-templates-list',
  templateUrl: './entries-templates-list.component.html',
  styleUrls: ['./entries-templates-list.component.css'],
  providers: [EntryTemplateService]
})
export class EntriesTemplatesListComponent implements OnInit {
  
  public model = new EntriesTemplates();
  public showFilters = true;
  public searchAsYouTypeSubject = new Subject();
  public searchAsYouTypeObservable = new Subscription();
  public types = EntryType.All;

  constructor(
    private service: EntryTemplateService,
    private alertService: AlertService,
    private router: Router) { }

  ngOnInit() {
    this.search();

    this.searchAsYouTypeObservable = this.searchAsYouTypeSubject
      .pipe(debounceTime(300))
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
          this.model.templates = dto.templates; //refresh
          this.sort();
          this.alertService.success('Done searching.');
        }
      });
  }

  sort() {
    this.model.templates.sort((a, b) => {
      return a.title < b.title ? -1 : 1
    });
  }

  createNew(): void {
    let template = new EntryTemplate();
    this.model.templates.push(template);
  }

  delete(template: EntryTemplate, index: number): void {
    if (template.autoId > 0) {
      this.service.delete(template.autoId).subscribe(dto => {
        if (dto.response.hasException) {
          this.alertService.error(dto.response.exception);
          template.status.setError(dto.response.exception);
        }
        else {
          this.model.templates.splice(index, 1);
          this.alertService.success('Delete ok!');
        }
      });
    }
    else {
      this.model.templates.splice(index, 1);
    }
  }

  saveIfValid(template: EntryTemplate): void {
    if (this.isValid(template))
      this.save(template);
  }

  isValid(template: EntryTemplate): boolean {
    //sera que nao da pra resolver isso com form validation?
    return template
      && template !== null
      && template.title
      && template.title !== null
      && template.title != '';
  }

  save(template: EntryTemplate): void {
    this.service.update(template).subscribe(dto => {
      if (dto.response.hasException) {
        this.alertService.error(dto.response.exception);
        template.status.setError(dto.response.exception);
      }
      else {
        this.alertService.success('Update ok!');
        this.refresh(dto, template);
        template.status.setNonError('Update @ ' + new Date());
      }
    });
  }

  refresh(from: EntryTemplate, to: EntryTemplate) {
    to.autoId = from.autoId;
    to.creationDateTime = from.creationDateTime;
    to.creationUser = from.creationUser;
    to.lastUpdateDateTime = from.lastUpdateDateTime;
    to.lastUpdateUser = from.lastUpdateUser;
    to.presentation = from.presentation;
    to.account = from.account;
    to.center = from.center;
    to.memo = from.memo;
    to.title = from.title;
    to.value = from.value;
    to.user = from.user;
    to.version = from.version;
    to.status = new EntityStatus();
  }

}

//TODO salvar somente se realmente teve modificacao
//TODO smart-search ainda cheio de problemas
