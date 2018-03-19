import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { Observable } from 'rxjs/Observable';
import { Subject } from 'rxjs/Subject';
import { Subscription } from 'rxjs/Subscription';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';

import { AlertService } from '../../core/local-services/alert.service';
import { Account } from '../../model/account.model';
import { Accounts } from '../../model/accounts.model';
import { EntryType } from '../../model/entryType.model';
import { EntityStatus } from '../../model/entityStatus.model';
import { AccountService } from '../../services/account.service';

@Component({
  selector: 'accounts-list',
  templateUrl: './accounts-list.component.html',
  styleUrls: ['./accounts-list.component.css'],
  providers: [AccountService]
})
export class AccountsListComponent implements OnInit {

  public model = new Accounts();
  public showFilters = true;
  public searchAsYouTypeSubject = new Subject();
  public searchAsYouTypeObservable = new Subscription(); //new Observable<string>();
  public types = EntryType.All;

  constructor(
    private service: AccountService,
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
          this.model.accounts = dto.accounts; //refresh
          this.sort();
          this.alertService.success('Done searching.');
        }
      });
  }

  sort() {
    this.model.accounts.sort((a, b) => {
      return a.structure < b.structure ? -1 : 1
    });
  }

  createNew(): void {
    let account = new Account();
    account.type = EntryType.Debit; //it does not matter
    this.model.accounts.push(account);
  }

  delete(account: Account, index: number): void {
    if (account.autoId > 0) {
      this.service.delete(account.autoId).subscribe(dto => {
        if (dto.response.hasException) {
          this.alertService.error(dto.response.exception);
          account.status.setError(dto.response.exception);
        }
        else {
          this.model.accounts.splice(index, 1);
          this.alertService.success('Delete ok!');
        }
      });
    }
    else {
      this.model.accounts.splice(index, 1);
    }
  }

  saveIfValid(account: Account): void {
    if (this.isValid(account))
      this.save(account);
  }

  isValid(account: Account): boolean {
    //sera que nao da pra resolver isso com form validation?
    return account
      && account !== null
      && account.description
      && account.description !== null
      && account.description != ''
      && account.structure
      && account.structure !== null
      && account.structure != '';
  }

  save(account: Account): void {
    this.service.update(account).subscribe(dto => {
      if (dto.response.hasException) {
        this.alertService.error(dto.response.exception);
        account.status.setError(dto.response.exception);
      }
      else {
        this.alertService.success('Update ok!');
        this.refresh(dto, account);
        account.status.setNonError('Update @ ' + new Date());
      }
    });
  }

  refresh(from: Account, to: Account) {
    to.autoId = from.autoId;
    to.creationDateTime = from.creationDateTime;
    to.creationUser = from.creationUser;
    to.description = from.description;
    to.lastUpdateDateTime = from.lastUpdateDateTime;
    to.lastUpdateUser = from.lastUpdateUser;
    to.presentation = from.presentation;
    to.structure = from.structure;
    to.type = from.type;
    to.user = from.user;
    to.version = from.version;
    to.status = new EntityStatus();
  }

}

//TODO red new/unsaved; orange edit/unsaved; yellow pinned; white else
//creating ---(update)---> pinned <---(pin/unpin)---> viewing ---(change)---> editing
//                         pinned <------------------------------(update)-----+
//                              +--------------------------------(change)---> editing
//por enquanto abandonei esta ideia

