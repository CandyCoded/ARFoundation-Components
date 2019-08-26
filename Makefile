test:
	echo "Error: no tests specified"

clean:
	git clean -xdf

deploy:
	git subtree push --prefix Assets/Plugins/ARFoundation-Components origin upm

deploy-force:
	git push origin `git subtree split --prefix Assets/Plugins/ARFoundation-Components master`:upm --force
