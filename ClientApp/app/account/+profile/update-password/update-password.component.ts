import { Component, OnInit, EventEmitter } from '@angular/core';

import { NotificationsService } from '../../../core';

import { ControlBase, ControlTextbox } from '../../../shared';
import { ProfileService } from '../profile.service';
import { UpdatePasswordModel } from '../profile.models';

@Component({
  selector: 'appc-update-password',
  templateUrl: './update-password.component.html',
  styleUrls: ['./update-password.component.scss'],
})
export class UpdatePasswordComponent implements OnInit {
  public errors: string[];
  public controls: Array<ControlBase<string>> = [
    new ControlTextbox({
      key: 'oldPassword',
      label: 'Current password',
      placeholder: 'Current password',
      value: '',
      type: 'password',
      order: 1
    }),
    new ControlTextbox({
      key: 'newPassword',
      label: 'New password',
      placeholder: 'New password',
      value: '',
      type: 'password',
      required: true,
      order: 2
    }),
    new ControlTextbox({
      key: 'confirmPassword',
      label: 'Verify password',
      placeholder: 'Verify password',
      value: '',
      type: 'password',
      required: true,
      order: 3
    })
  ];

  public reset = new EventEmitter<boolean>();
  constructor(public profileService: ProfileService, private ns: NotificationsService) { }

  public ngOnInit() { }

  public save(model: UpdatePasswordModel): void {
    this.profileService.changePassword(model)
      .subscribe(res => {
        this.reset.emit(true);
        this.ns.success('Password changed successfully');
      }, err => {
        debugger;
        console.log(err);
        this.ns.error(err.error)
      });
  }
}
