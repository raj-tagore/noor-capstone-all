# 11 — Update doctor

Commit: `UpdateDoctorDetails implemented`. PS: `ps/14`.

## Read the URL

```typescript
ngOnInit() {
  this.doctorId = Number(this.route.snapshot.params['doctorId']);
  this.doctorName = this.route.snapshot.params['doctorName'];
}
```

`[(ngModel)]="doctorName"` shows that name in the text box. Doctor id input is `readonly`.

## Save

```typescript
editDoctorDetails(doctorname: string) {
  this._cureWellService.editDoctorDetails(this.doctorId, doctorname).subscribe({
    next: (response) => {
      this.status = response;
      if (response) {
        alert("Doctor's name updated successfully!");
      } else {
        alert("Doctor's name not updated");
      }
      this.router.navigate(['/viewDoctors']);
    },
    error: (error) => {
      this.errorMsg = error;
      alert('Some error occurred');
      this.router.navigate(['/viewDoctors']);
    },
    complete: () => {
      console.log('Updated doctor details successfully.');
    }
  });
}
```

Form: `(ngSubmit)="editDoctorDetails(doctorName)"`.

Both the true and false branches navigate to `ViewDoctor`. The error branch does too. `complete` logs only after success.

## Exam variations

1. Navigate only when `response` is true, and stay on the form when it is false — only if the table says that. This table says navigate in both branches.
2. Alert text must match the table, including `Doctor's`.
