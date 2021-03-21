import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Subject } from 'rxjs';
import { Subscription } from 'rxjs';
import { debounceTime } from 'rxjs/operators';

import { AlertService } from '../../core/local-services/alert.service';
import { ResultCenter } from '../../model/result-center.model';
import { ResultsCenters } from '../../model/results-centers.model';
import { EntityStatus } from '../../model/entityStatus.model';
import { ResultCenterService } from '../../services/result-center.service';

@Component({
  selector: 'results-centers-list',
  templateUrl: './results-centers-list.component.html',
  styleUrls: ['./results-centers-list.component.css'],
  providers: [ResultCenterService]
})
export class ResultsCentersListComponent implements OnInit {

  public model = new ResultsCenters();
  public showFilters = true;
  public searchAsYouTypeSubject = new Subject();
  public searchAsYouTypeObservable = new Subscription();

  constructor(
    private service: ResultCenterService,
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
          this.model.resultsCenters = dto.resultsCenters; //refresh
          this.sort();
          this.alertService.success('Done searching.');
        }
      });
  }

  sort() {
    this.model.resultsCenters.sort((a, b) => {
      return a.code < b.code ? -1 : 1
    });
  }

  createNew(): void {
    let rc = new ResultCenter();
    this.model.resultsCenters.push(rc);
  }

  delete(rc: ResultCenter, index: number): void {
    if (rc.autoId > 0) {
      this.service.delete(rc.autoId).subscribe(dto => {
        if (dto.response.hasException) {
          this.alertService.error(dto.response.exception);
          rc.status.setError(dto.response.exception);
        }
        else {
          this.model.resultsCenters.splice(index, 1);
          this.alertService.success('Delete ok!');
        }
      });
    }
    else {
      this.model.resultsCenters.splice(index, 1);
    }
  }

  saveIfValid(rc: ResultCenter): void {
    if (this.isValid(rc))
      this.save(rc);
  }

  isValid(rc: ResultCenter): boolean {
    //sera que nao da pra resolver isso com form validation?
    return rc
      && rc !== null
      && rc.description
      && rc.description !== null
      && rc.description != ''
      && rc.code
      && rc.code !== null
      && rc.code != '';
  }

  save(rc: ResultCenter): void {
    this.service.update(rc).subscribe(dto => {
      if (dto.response.hasException) {
        this.alertService.error(dto.response.exception);
        rc.status.setError(dto.response.exception);
      }
      else {
        this.alertService.success('Update ok!');
        this.refresh(dto, rc);
        rc.status.setNonError('Update @ ' + new Date());
      }
    });
  }

  refresh(from: ResultCenter, to: ResultCenter) {
    to.autoId = from.autoId;
    to.code = from.code;
    to.creationDateTime = from.creationDateTime;
    to.creationUser = from.creationUser;
    to.description = from.description;
    to.lastUpdateDateTime = from.lastUpdateDateTime;
    to.lastUpdateUser = from.lastUpdateUser;
    to.presentation = from.presentation;
    to.user = from.user;
    to.version = from.version;
    to.status = new EntityStatus();
  }

}
