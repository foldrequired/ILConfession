import { Injectable } from '@angular/core';
import { CanDeactivate } from '@angular/router';
import { MemberProfileComponent } from '../members/member-profile/member-profile.component';

@Injectable()
export class UnsavedChanges implements CanDeactivate<MemberProfileComponent> {
  canDeactivate(component: MemberProfileComponent) {
    if (component.editForm.dirty) {
      return confirm('Please save your changes before navigating out');
    }

    return true;
  }
}
