# 07 — View doctors

Commit: `View Doctors works`. PS table: `ps/16` rows `ngOnInit` and `getDoctor`. Remove is [13](13-delete-doctor.md).

## ngOnInit

```typescript
ngOnInit() {
  this.getDoctor();
}
```

## getDoctor

```typescript
getDoctor() {
  this._curewellService.getDoctors().subscribe({
    next: (res) => {
      this.doctorList = res;
      this.showMsgDiv = true;
      console.log('Doctors Fetched Successfully');
    },
    error: (error) => {
      this.doctorList = null;
      this.errorMsg = error;
      this.showMsgDiv = true;
    }
  });
}
```

| PS | Code |
|---|---|
| call service | `getDoctors()` |
| success → `doctorList` | `this.doctorList = res` |
| set `showMsgDiv` | `true` in both handlers |
| error → list `null`, `errorMsg` | `error` callback |
| console text | `'Doctors Fetched Successfully'` |

## Edit button → update page

```typescript
editDoctorDetails(doctor: Doctor) {
  this.router.navigate(['/editDoctorDetails', doctor.doctorId, doctor.doctorName]);
}
```

HTML: `(click)="editDoctorDetails(doctor)"` inside `*ngFor="let doctor of doctorList"`.

## Exam variations

1. Move the `console.log` into `complete` if the table says “on completion”.
2. `showMsgDiv = false` on success if the empty-state `*ngIf` is tied to that flag (specialization HTML works that way; this doctor HTML keys off `doctorList?.length`).
