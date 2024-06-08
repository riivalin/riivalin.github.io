---
layout: post
title: "[Issue] Jekyll deployed in github shows raw text of index.html file"
date: 2023-02-26 01:10:00 +0800
categories: [Notes,Jekyll,GitHub]
tags: [Jekyll Blog,GitHub Action, Github Pages]
---

### [Problem Description]

Deploy the Jekyll theme website using the forked repo, on my local system, all works fine.
Even Github build status also indicates that the site is up and running at https://username.github.io. 
However, when I access https://username.github.io from browser...

The following line is displayed:
```html
--- layout: home # Index page ---
```

### [Issue Solved]

For me, it works fine with configuring Source as Github Actions.

![img-description](/assets/img/github-actions.png)

Change setting to deploy source = github actions.

Path:
Setting > Pages (Github Pages)> Build and deployment > Source > GitHub Actions.

ref:
- [https://github.com/cotes2020/jekyll-theme-chirpy/issues/628](https://github.com/cotes2020/jekyll-theme-chirpy/issues/628)
- [https://stackoverflow.com/questions/72079476/jekyll-deployed-in-github-shows-raw-text-of-index-html-file](https://stackoverflow.com/questions/72079476/jekyll-deployed-in-github-shows-raw-text-of-index-html-file)
