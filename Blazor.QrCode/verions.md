#How do you control the version number?

Versionize uses conventional commits to determine the version of your release.

This is a runthrough of how it works
```
> git add .
> git commit -m "fix: trim email addresses"
> versionize
> git push --follow-tags origin main
```
When you do a push, versionize will create a tag and update the application patch version. So, from 1.0.0 to 1.0.1.

Now let’s add a new major change, a breaking change

```
> git add .
> git commit -m "feat!: trim email addresses"
> versionize
> git push --follow-tags origin main
```

When you do a push, versionize will create a tag and update the application patch version. So, from 1.0.1 to 2.0.0.

Now if you want to add a normal feature, you can just do

```
> git add .
> git commit -m "feat: added email queueing"
> versionize
> git push --follow-tags origin main
```

When you do a push, versionize will create a tag and update the application patch version. So, from 2.0.0 to 2.0.1.