import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { SubnetService } from '../services/subnet.service';
import { MatSnackBar } from '@angular/material/snack-bar';

@Component({
  selector: 'app-new-subnet',
  templateUrl: './new-subnet.component.html',
  styleUrl: './new-subnet.component.css'
})
export class NewSubnetComponent {
  form: FormGroup;
  constructor(private fb: FormBuilder, private subnetService: SubnetService, private snackBar: MatSnackBar) {
    this.form = this.fb.group({
      cidr: ["", [Validators.required, Validators.pattern("^(([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])\.){3}([0-9]|[1-9][0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])(\/(3[0-2]|[1-2][0-9]|[0-9]))$")]]
    });
  }

  onSubmit() {
    if (this.form.invalid) return;
    this.form.disable();
    const cidr = this.form.get("cidr")?.value;
    this.subnetService.createSubnet(cidr).subscribe(
      null,
      (_) => {
        this.snackBar.open("Something Went Wrong!", "Close", {
          duration: 5000,
        });
        this.form.enable();
      },
      () => {
        this.snackBar.open(`Subnet ${cidr} Created!`, "Close", {
          duration: 5000,
        });
        this.form.reset();
        this.form.enable();
        this.form?.get("cidr")?.setErrors(null);
        this.form.setErrors(null);
      }
    );
  }
}
